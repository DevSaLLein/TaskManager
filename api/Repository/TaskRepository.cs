using TaskManager.Helpers;
using TaskManager.Model;
using api.DTO.Tarefa.Response;
using api.DTO;
using TasManager.DTO.Account.Response;
using api.DTO.Tarefa.Request;

namespace TaskManager.Repository
{
    public class TaskRepository(TaskManagerContext Database, IViaCepIntegracao viaCep) : ITaskRepository
    {
        private readonly TaskManagerContext _database = Database;
        private readonly IViaCepIntegracao _viaCep = viaCep;

        public async Task<TaskItem> CreateTask(TarefaCreateRequest Dto, string UserName, CancellationToken Token)
        {
            TaskItem Task = new TaskItem(Dto.Nome);

            await _database.Tasks.AddAsync(Task, Token);

            var TaskRecentCreated = await GetOneTaskByObject(Task);

            await _database.SaveChangesAsync(Token);

            var isSuccess = await CreateRelationUserWithTheTask(UserName, TaskRecentCreated.Id, Token);

            if(!isSuccess) return null;

            return Task;
        }

        public async Task<TaskItem> DeleteTask(Guid Id, CancellationToken Token)
        {
            var TaskIsFound = await GetOneTask(Id, Token);

            if(TaskIsFound != null) 
            {
                _database.Tasks.Remove(TaskIsFound);

                await _database.SaveChangesAsync(Token);
            }
        
            return TaskIsFound;
        }
        
        public async Task<List<GetAllUsersWithYoursTarefas>> GetAllTasks(QueryObjectFilter Filter, CancellationToken Token)
        {
            var Tasks = _database.UserTasks
                .Include(ut => ut.User)
                .Include(ut => ut.Task)
                .AsNoTracking()
                .AsQueryable()
            ;

            if(Filter.Status != null)
                Tasks = Tasks.Where(Entity => Entity.Task.Status == Filter.Status)
            ;

            if(Filter.isSortByData)
                Tasks = Tasks.OrderByDescending(Task => Task.Task.Data);
            ;
            
            var TasksList = await Tasks
                .GroupBy(ut => ut.UserId)
                .Select(group => new GetAllUsersWithYoursTarefas
                (
                    group.Select( Entity => new UserInformationsToTarefas
                    (
                        Entity.User.UserName,
                        Entity.User.Email
                    )).FirstOrDefault(),
                    
                    group.Select(ut => ut.Task).ToList()
                ))
                .Skip((Filter.PageNumber - 1) * Filter.PageSize)
                .Take(Filter.PageSize)
                .ToListAsync(Token)
            ;   

            return TasksList;
        }

        public async Task<GetAllResponsePaged<List<TaskItem>>> GetAllTasksFromUser(PagedRequest request)
        {
            var user = await _database
                .Users
                .Where(x => x.UserName == request.UserName)
                .FirstOrDefaultAsync()
            ;   

            var query = _database
                .UserTasks
                .AsNoTracking()
                .Where(x => x.UserId == user.Id)
                .OrderBy(x => x.Task.Nome)
            ;

            var tasks = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => x.Task)
                .ToListAsync()
            ;

            var count = await query.CountAsync(); 

            var localidade = await _viaCep.ObterDadosViaCep(user.Cep);

            return new GetAllResponsePaged<List<TaskItem>>
            (
                new UserInformationsToTarefas
                (
                    user.UserName, 
                    user.Email
                ),
                localidade,
                tasks,
                count,
                request.PageNumber,
                request.PageSize   
            );     
        }

        public async Task<TaskItem> GetOneTask(Guid Id, CancellationToken Token)
        {
            TaskItem TaskIsFound = await _database.Tasks.SingleOrDefaultAsync(task => task.Id == Id, cancellationToken: Token);

            if(TaskIsFound == null) return null;

            return TaskIsFound;
        }

        public async Task<TaskItem> GetOneTaskByObject(TaskItem taskItem)
        {
            return await _database.Tasks.FindAsync(taskItem.Id);
        }

        public async Task<TaskItem> UpdateTask(TarefaUpdateRequest dto, Guid Id, CancellationToken Token)
        {
            var TaskIsFound = await GetOneTask(Id, Token);
            return TaskIsFound;
        }

        private async Task<bool> CreateRelationUserWithTheTask(string UserName, Guid IdFromTask, CancellationToken Token)
        {
            var User = await _database.Users.FirstOrDefaultAsync(Entity => Entity.UserName == UserName ,cancellationToken: Token);
            Guid IdFromUser = Guid.Parse(User.Id);

            var newRelation = await _database.UserTasks.AddAsync(new UserTasks { UserId = IdFromUser.ToString(), TaskId = IdFromTask }, cancellationToken: Token);       
            
            if(newRelation == null) return false;

            await _database.SaveChangesAsync(Token);

            return true;
        } 
    }
}
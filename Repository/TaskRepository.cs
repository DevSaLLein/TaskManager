using Microsoft.EntityFrameworkCore;
using TaskManager.Context;
using TaskManager.Helpers;
using TaskManager.Interfaces;
using TaskManager.Model;
using TaskManager.DTO;
using TasManager.Models;
using TasManager.DTO.Response.User;
using ConsumoDeAPIs;

namespace TaskManager.Repository
{
    public class TaskRepository(TaskManagerContext Database, ViaCepIntegracao ViaCep ) : ITaskRepository
    {
        private readonly TaskManagerContext _database = Database;
        private readonly ViaCepIntegracao _viaCep = ViaCep;

        public async Task<TaskItem> CreateTask(TaskCreateRequestDto Dto, string UserName, CancellationToken Token)
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
        
        public async Task<List<GetAllUsersWithYoursTasksDto>> GetAllTasks(QueryObjectFilter Filter, CancellationToken Token)
        {
            var Tasks = _database.UserTasks
                .Include(ut => ut.User)
                .Include(ut => ut.Task)
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
                .Select(group => new GetAllUsersWithYoursTasksDto
                (
                    group.Select( Entity => new UserInformationsToTasksDto
                    (
                        Entity.User.UserName,
                        Entity.User.Email,
                        Entity.User.Cep
                        
                    )).FirstOrDefault(),
                    group.Select(ut => ut.Task).ToList()
                ))
                .Skip((Filter.PageNumber - 1) * Filter.PageSize)
                .Take(Filter.PageSize)
                .ToListAsync(Token)
            ;   

            return TasksList;
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

        public async Task<TaskItem> UpdateTask(TaskUpdateRequestDto dto, Guid Id, CancellationToken Token)
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
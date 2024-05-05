using Microsoft.EntityFrameworkCore;
using TaskManager.Context;
using TaskManager.Helpers;
using TaskManager.Interface;
using TaskManager.Model;
using TaskManager.DTO;

namespace TaskManager.Repository
{
    public class TaskRepository(TaskManagerContext Database) : ITaskRepository
    {
        private readonly TaskManagerContext _database = Database;

        public async Task<TaskItem> CreateTask(TaskCreateRequestDto Dto, CancellationToken Token)
        {
            TaskItem Task = new TaskItem(Dto.Nome, Dto.IdUser);

            await _database.AddAsync(Task, Token);
            await _database.SaveChangesAsync(Token);

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
        
        public async Task<List<TaskItem>> GetAllTasks(QueryObjectFilter Filter, CancellationToken Token)
        {
            var Tasks = _database.Tasks.AsQueryable();

            if(Filter.Status != null)
                Tasks = Tasks.Where(Entity => Entity.Status == Filter.Status)
            ;

            return await Tasks.ToListAsync(Token);
        }

        public async Task<TaskItem> GetOneTask(Guid Id, CancellationToken Token)
        {
            TaskItem TaskIsFound = await _database.Tasks.SingleOrDefaultAsync(task => task.Id == Id, cancellationToken: Token);

            if(TaskIsFound == null) return null;

            return TaskIsFound;
        }

        public async Task<UsuárioModel> GetTaskItemsByUser(QueryObjectFilter Filter, Guid IdUser, CancellationToken Token)
        {
            var Query = _database.Usuarios
                .Include(Entity => Entity.Tasks)
                .Where(Entity => Entity.Id == IdUser)
                .AsQueryable()
            ;

            var UserWithYoursTasks = await Query.SingleOrDefaultAsync(Token);

            if(Filter.Status != null)
                UserWithYoursTasks.Tasks = UserWithYoursTasks.Tasks.Where(task => task.Status == Filter.Status).ToList()
            ;

            return UserWithYoursTasks;
        }

        public async Task<TaskItem> UpdateTask(TaskUpdateRequestDto dto, Guid Id, CancellationToken Token)
        {
            var TaskIsFound = await GetOneTask(Id, Token);
            
            TaskIsFound?.UpdateTask(dto.Nome);

            await _database.SaveChangesAsync(Token);

            return TaskIsFound;
        }
    }
}
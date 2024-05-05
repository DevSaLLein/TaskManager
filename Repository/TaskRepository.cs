using Microsoft.EntityFrameworkCore;
using TaskManager.Context;
using TaskManager.DTO;
using TaskManager.Interface;
using TaskManager.Model;

namespace TaskManager.Repository
{
    public class TaskRepository(TaskManagerContext Database) : ITaskRepository
    {
        private readonly TaskManagerContext _database = Database;

        public async Task<TaskItem> CreateTask(TaskRequestDto Dto, CancellationToken Token)
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
        
        public async Task<List<TaskItem>> GetAllTasks(CancellationToken Token)
        {
            return await _database.Tasks.ToListAsync(Token);
        }

        public async Task<TaskItem> GetOneTask(Guid Id, CancellationToken Token)
        {
            TaskItem TaskIsFound = await _database.Tasks.SingleOrDefaultAsync(task => task.Id == Id, cancellationToken: Token);

            if(TaskIsFound == null) return null;

            return TaskIsFound;
        }

        public async Task<UsuárioModel> GetTaskItemsByUser(Guid IdUser, CancellationToken Token)
        {
            var UserWithYoursTasks = await _database.Usuarios
                .Include(Entity => Entity.Tasks)
                .SingleOrDefaultAsync(Entity => Entity.Id == IdUser, cancellationToken: Token) 
            ;

            return UserWithYoursTasks;
        }

        public async Task<TaskItem> UpdateTask(TaskRequestDto dto, Guid Id, CancellationToken Token)
        {
            var TaskIsFound = await GetOneTask(Id, Token);
            
            TaskIsFound?.UpdateTask(dto.Nome);

            await _database.SaveChangesAsync(Token);

            return TaskIsFound;
        }
    }
}
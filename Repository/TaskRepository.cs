using Microsoft.EntityFrameworkCore;
using TaskManager.Context;
using TaskManager.DTO;
using TaskManager.Interface;
using TaskManager.Model;

namespace TaskManager.Repository
{
    public class TaskRepository(TaskManagerContext database) : ITaskRepository
    {
        private readonly TaskManagerContext _database = database;

        public async Task<TaskItem> CreateTask(TaskRequestDto dto, CancellationToken token)
        {
            TaskItem task = new TaskItem(dto.Nome, dto.idUser);

            await _database.AddAsync(task, token);
            await _database.SaveChangesAsync(token);

            return task;
        }

        public async Task<TaskItem?> DeleteTask(Guid Id, CancellationToken token)
        {
            var taskIsFound = await GetOneTask(Id, token);

            if(taskIsFound != null) 
            {
                _database.Tasks.Remove(taskIsFound);

                await _database.SaveChangesAsync(token);
            }
        
            return taskIsFound;
        }
        
        public async Task<List<TaskItem>> GetAllTasks(CancellationToken token)
        {
            return await _database.Tasks.ToListAsync(token);
        }

        public async Task<TaskItem?> GetOneTask(Guid Id, CancellationToken token)
        {
            TaskItem? taskIsFound = await _database.Tasks.SingleOrDefaultAsync(task => task.Id == Id, cancellationToken: token);

            if(taskIsFound == null) return null;

            return taskIsFound;
        }

        public async Task<List<TaskItem>> GetTaskItemsByUser(Guid IdUser, CancellationToken token)
        {
            var tasks = await _database.Tasks
                .Include(task => task.Login)
                .ToListAsync(token)
            ;

            return tasks;
        }

        public async Task<TaskItem?> UpdateTask(TaskRequestDto dto, Guid Id, CancellationToken token)
        {
            var taskIsFound = await GetOneTask(Id, token);
            
            taskIsFound?.UpdateTask(dto.Nome);

            await _database.SaveChangesAsync(token);

            return taskIsFound;
        }
    }
}
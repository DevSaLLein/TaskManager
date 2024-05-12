using Microsoft.EntityFrameworkCore;
using TaskManager.Context;
using TaskManager.Helpers;
using TaskManager.Interfaces;
using TaskManager.Model;
using TaskManager.DTO;
using Microsoft.AspNetCore.Identity;
using TasManager.Models;

namespace TaskManager.Repository
{
    public class TaskRepository(UserManager<UserIdentityApp> UserIdentity, TaskManagerContext Database) : ITaskRepository
    {
        private readonly TaskManagerContext _database = Database;
        private readonly UserManager<UserIdentityApp> _user = UserIdentity;

        public async Task<TaskItem> CreateTask(TaskCreateRequestDto Dto, string UserName, CancellationToken Token)
        {
            TaskItem Task = new TaskItem(Dto.Nome);

            var User = await _database.Users.FirstOrDefaultAsync(Entity => Entity.UserName == UserName ,cancellationToken: Token);
            Guid IdFromUser = Guid.Parse(User.Id);

            await _database.Tasks.AddAsync(Task, Token);

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

            if(Filter.isSortByData)
                Tasks = Tasks.OrderByDescending(Task => Task.Data); 
            ;

            var SkipNumber = (Filter.PageNumber - 1) * Filter.PageSize;

            return await Tasks.Skip(SkipNumber).Take(Filter.PageSize).ToListAsync(Token);
        }

        public async Task<TaskItem> GetOneTask(Guid Id, CancellationToken Token)
        {
            TaskItem TaskIsFound = await _database.Tasks.SingleOrDefaultAsync(task => task.Id == Id, cancellationToken: Token);

            if(TaskIsFound == null) return null;

            return TaskIsFound;
        }

        public async Task<TaskItem> UpdateTask(TaskUpdateRequestDto dto, Guid Id, CancellationToken Token)
        {
            var TaskIsFound = await GetOneTask(Id, Token);
            return TaskIsFound;
        }

        private async Task<TaskItem> CreateRelationUserWithTheTask(string username, Guid IdFromTask)
        {
            return null;       
        } 
    }
}
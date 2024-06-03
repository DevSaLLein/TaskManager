using Microsoft.EntityFrameworkCore;
using TaskManager.Context;
using TaskManager.Model;
using TasManager.Interfaces;
using TasManager.Models;

namespace TasManager.Repository
{
    public class UserRepository(TaskManagerContext Database) : IUserRepository
    {
        private readonly TaskManagerContext _database = Database;

        public async Task<List<TaskItem>> getAllTasks(UserIdentityApp user)
        {
            return await _database.UserTasks
                .Where(Entity => Entity.UserId == user.Id)
                .Select(taskItem => new TaskItem (taskItem.TaskId, taskItem.Task.Nome, taskItem.Task.Status, taskItem.Task.Data )).ToListAsync()
            ;
        }
    }
}
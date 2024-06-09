using TaskManager.Model;

namespace TasManager.Repository
{
    public class UserRepository(TaskManagerContext Database) : IUserRepository
    {
        private readonly TaskManagerContext _database = Database;

        public async Task<List<TaskItem>> GetAllTasks(UserIdentityApp user)
        {
            return await _database.UserTasks
                .Where(Entity => Entity.UserId == user.Id)
                .Select(taskItem => new TaskItem (taskItem.TaskId, taskItem.Task.Nome, taskItem.Task.Status, taskItem.Task.Data )).ToListAsync()
            ;
        }
    }
}
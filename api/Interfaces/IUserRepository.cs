using TaskManager.Model;

namespace TasManager.Interfaces
{
    public interface IUserRepository
    {
        Task<List<TaskItem>> getAllTasks(UserIdentityApp user);       
    }
}
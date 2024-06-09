using TaskManager.Model;

namespace TasManager.Interfaces
{
    public interface IUserRepository
    {
        Task<List<TaskItem>> GetAllTasks(UserIdentityApp user);       
    }
}
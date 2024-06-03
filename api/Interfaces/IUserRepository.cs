using TaskManager.Model;
using TasManager.Models;

namespace TasManager.Interfaces
{
    public interface IUserRepository
    {
        Task<List<TaskItem>> getAllTasks(UserIdentityApp user);       
    }
}
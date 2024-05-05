using TaskManager.DTO;
using TaskManager.Model;

namespace TaskManager.Interface
{
    public interface ITaskRepository
    {
        Task<TaskItem> CreateTask(TaskRequestDto Dto, CancellationToken Token);

        Task<TaskItem> DeleteTask(Guid Id, CancellationToken Token);

        Task<TaskItem> UpdateTask(TaskRequestDto Dto, Guid Id, CancellationToken Token);

        Task<List<TaskItem>> GetAllTasks(CancellationToken Token);

        Task<TaskItem> GetOneTask(Guid Id, CancellationToken Token);

        // By User

        Task<UsuárioModel> GetTaskItemsByUser(Guid IdUser, CancellationToken Token);
    }
}
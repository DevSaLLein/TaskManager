using TaskManager.DTO;
using TaskManager.Model;

namespace TaskManager.Interface
{
    public interface ITask
    {
        Task<TaskItem> CreateTask(TaskRequestDto dto, CancellationToken token);

        Task<TaskItem?> DeleteTask(Guid Id, CancellationToken token);

        Task<TaskItem?> UpdateTask(TaskRequestDto dto, Guid Id, CancellationToken token);

        Task<List<TaskItem>> GetAllTasks(CancellationToken token);

        Task<TaskItem?> GetOneTask(Guid Id, CancellationToken token);
    }
}
using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.DTO;

namespace TaskManager.Interface
{
    public interface ITaskRepository
    {
        Task<TaskItem> CreateTask(TaskCreateRequestDto Dto, CancellationToken Token);

        Task<TaskItem> DeleteTask(Guid Id, CancellationToken Token);

        Task<TaskItem> UpdateTask(TaskUpdateRequestDto Dto, Guid Id, CancellationToken Token);

        Task<List<TaskItem>> GetAllTasks(QueryObjectFilter Filter, CancellationToken Token);

        Task<TaskItem> GetOneTask(Guid Id, CancellationToken Token);

        // By User

        Task<UsuárioModel> GetTaskItemsByUser(QueryObjectFilter Filter, Guid IdUser, CancellationToken Token);
    }
}
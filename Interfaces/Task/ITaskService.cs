using TaskManager.Helpers;
using TaskManager.DTO;

namespace TaskManager.Interfaces
{
    public interface ITaskService
    {
        Task<Guid> CreateTask(TaskCreateRequestDto Dto, CancellationToken Token);

        Task<bool> UpdateTask(Guid Id, TaskUpdateRequestDto Dto, CancellationToken Token);       

        Task<List<TaskResponseDto>> GetAllTasks(QueryObjectFilter Filter, CancellationToken Token);

        Task<TaskResponseDto> GetOneTask(Guid Id, CancellationToken Token);
        
        Task<bool> DeleteTask(Guid Id, CancellationToken Token);
    }
}
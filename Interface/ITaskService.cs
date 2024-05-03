using TaskManager.DTO;

namespace TaskManager.Interface
{
    public interface ITaskService
    {
        Task<bool> CreateTask(TaskRequestDto dto, CancellationToken token);
        Task<bool> UpdateTask(Guid id, TaskRequestDto dto, CancellationToken token);       
        Task<List<TaskResponseDto>> GetAllTasks(CancellationToken token);
        Task<TaskResponseDto> GetOneTask(Guid id, CancellationToken token);
        Task<bool> DeleteTask(Guid id, CancellationToken token);
    }
}
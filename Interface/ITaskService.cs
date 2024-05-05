using TaskManager.DTO;
using TaskManager.Helpers;

namespace TaskManager.Interface
{
    public interface ITaskService
    {
        Task<Guid> CreateTask(TaskRequestDto Dto, CancellationToken Token);

        Task<bool> UpdateTask(Guid Id, TaskRequestDto Dto, CancellationToken Token);       

        Task<List<TaskResponseDto>> GetAllTasks(QueryObjectFilter Filter, CancellationToken Token);

        Task<TaskResponseDto> GetOneTask(Guid Id, CancellationToken Token);
        
        Task<bool> DeleteTask(Guid Id, CancellationToken Token);

        // By user

        Task<UserResponseDto> GetAllTasksByUserResponse(QueryObjectFilter Filter, Guid IdUser, CancellationToken Token);
    }
}
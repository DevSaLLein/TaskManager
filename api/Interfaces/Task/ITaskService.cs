using TaskManager.Helpers;
using TaskManager.DTO;
using TasManager.DTO.Response.User;

namespace TaskManager.Interfaces
{
    public interface ITaskService
    {
        Task<Guid> CreateTask(TaskCreateRequestDto Dto, string UserName, CancellationToken Token);

        Task<bool> UpdateTask(Guid Id, TaskUpdateRequestDto Dto, CancellationToken Token);       

        Task<List<GetAllUsersWithYoursTasksDto>> GetAllTasks(QueryObjectFilter Filter, CancellationToken Token);

        Task<TaskResponseDto> GetOneTask(Guid Id, CancellationToken Token);
        
        Task<bool> DeleteTask(Guid Id, CancellationToken Token);
    }
}
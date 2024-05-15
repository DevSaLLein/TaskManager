using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.DTO;
using TasManager.Models;
using TasManager.DTO.Response.User;

namespace TaskManager.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskItem> CreateTask(TaskCreateRequestDto Dto, string UserName, CancellationToken Token);

        Task<TaskItem> DeleteTask(Guid Id, CancellationToken Token);

        Task<TaskItem> UpdateTask(TaskUpdateRequestDto Dto, Guid Id, CancellationToken Token);

        Task<List<GetAllUsersWithYoursTasksDto>> GetAllTasks(QueryObjectFilter Filter, CancellationToken Token);

        Task<TaskItem> GetOneTask(Guid Id, CancellationToken Token);
    }
}
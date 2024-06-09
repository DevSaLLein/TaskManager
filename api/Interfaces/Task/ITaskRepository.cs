using api.DTO;
using api.DTO.Tarefa.Request;
using api.DTO.Tarefa.Response;
using TaskManager.Helpers;
using TaskManager.Model;
using TasManager.DTO.Account.Response;

namespace TaskManager.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskItem> CreateTask(TarefaCreateRequest request, string UserName, CancellationToken Token);

        Task<TaskItem> DeleteTask(Guid Id, CancellationToken Token);

        Task<TaskItem> UpdateTask(TarefaUpdateRequest request, Guid Id, CancellationToken Token);

        Task<List<GetAllUsersWithYoursTarefas>> GetAllTasks(QueryObjectFilter Filter, CancellationToken Token);

        Task<GetAllResponsePaged<List<TaskItem>>> GetAllTasksFromUser(PagedRequest request);

        Task<TaskItem> GetOneTask(Guid Id, CancellationToken Token);
    }
}
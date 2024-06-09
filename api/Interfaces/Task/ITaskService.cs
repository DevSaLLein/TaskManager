using api.DTO;
using api.DTO.Tarefa.Request;
using api.DTO.Tarefa.Response;
using TaskManager.DTO.Tarefa.Response;
using TaskManager.Helpers;
using TaskManager.Model;
using TasManager.DTO.Account.Response;

namespace TaskManager.Interfaces
{
    public interface ITaskService
    {
        Task<TarefaResponse> CreateTask(TarefaCreateRequest Dto, string UserName, CancellationToken Token);

        Task<bool> UpdateTask(Guid Id, TarefaUpdateRequest Dto, CancellationToken Token);       

        Task<List<GetAllUsersWithYoursTarefas>> GetAllTasks(QueryObjectFilter Filter, CancellationToken Token);

        Task<TarefaResponseWithUser> GetOneTask(Guid Id, CancellationToken Token);
        
        Task<bool> DeleteTask(Guid Id, CancellationToken Token);


        Task<GetAllResponsePaged<List<TaskItem>>> GetAllTasksFromUser(PagedRequest request);
    }
}
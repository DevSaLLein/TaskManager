using TaskManager.Helpers;
using TaskManager.Model;
using api.DTO.Tarefa.Request;
using api.DTO.Tarefa.Response;
using TasManager.DTO.Account.Response;
using TaskManager.DTO.Tarefa.Response;
using api.DTO;

namespace TaskManager.Service
{
    public class TaskService(ITaskRepository repository) : ITaskService
    {
        private readonly ITaskRepository _repository = repository;

        public async Task<TarefaResponse> CreateTask(TarefaCreateRequest dto, string UserName, CancellationToken token)
        {
            var Task = await _repository.CreateTask(dto, UserName, token);
            return new TarefaResponse {
                Id = Task.Id,
                Nome = Task.Nome,
                Status = Task.Status,
                Data = Task.Data
            };
        }

        public async Task<bool> UpdateTask(Guid id, TarefaUpdateRequest dto, CancellationToken token)
        {
            TaskItem Task = await _repository.UpdateTask(dto, id, token);

            if(Task != null) 
            {
                return true;
            }

            return false;
        }

        public async Task<List<GetAllUsersWithYoursTarefas>> GetAllTasks(QueryObjectFilter Filter, CancellationToken token)
        {
            List<GetAllUsersWithYoursTarefas> Tasks = await _repository.GetAllTasks(Filter, token);

            return Tasks;
        }

        public async Task<TarefaResponseWithUser> GetOneTask(Guid id, CancellationToken token)
        {
            TaskItem Task = await _repository.GetOneTask(id, token);

            var User = Task.UserTasks.Select(Entity => Entity.User).FirstOrDefault();
            var userInformations = new UserInformationsToTarefas (User.UserName, User.Email);
            
            if(Task != null)
            {
                var TaskResponse = new TarefaResponseWithUser(userInformations, Task.Nome, Task.Status, Task.Data);
                return TaskResponse;
            }

            return null;
        }

        public async Task<bool> DeleteTask(Guid id, CancellationToken token)
        {
            await _repository.DeleteTask(id, token);
            return true;
        }

        public async Task<GetAllResponsePaged<List<TaskItem>>> GetAllTasksFromUser(PagedRequest request)
        {
            var response = await _repository.GetAllTasksFromUser(request);

            return response;
        }
    }
}
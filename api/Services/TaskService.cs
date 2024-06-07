using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.DTO;
using TasManager.DTO.Response.User;

namespace TaskManager.Service
{
    public class TaskService(ITaskRepository repository) : ITaskService
    {
        private readonly ITaskRepository _repository = repository;

        public async Task<Guid> CreateTask(TaskCreateRequestDto dto, string UserName, CancellationToken token)
        {
            var Task = await _repository.CreateTask(dto, UserName, token);
            return Task.Id;
        }

        public async Task<bool> UpdateTask(Guid id, TaskUpdateRequestDto dto, CancellationToken token)
        {
            TaskItem Task = await _repository.UpdateTask(dto, id, token);

            if(Task != null) 
            {
                return true;
            }

            return false;
        }

        public async Task<List<GetAllUsersWithYoursTasksDto>> GetAllTasks(QueryObjectFilter Filter, CancellationToken token)
        {
            List<GetAllUsersWithYoursTasksDto> Tasks = await _repository.GetAllTasks(Filter, token);

            return Tasks;
        }

        public async Task<TaskResponseDto> GetOneTask(Guid id, CancellationToken token)
        {
            TaskItem Task = await _repository.GetOneTask(id, token);

            var User = Task.UserTasks.Select(Entity => Entity.User).FirstOrDefault();
            UserInformationsToTasksDto userInformations = new UserInformationsToTasksDto (User.UserName, User.Email, User.Cep);
            
            if(Task != null)
            {
                TaskResponseDto TaskResponse = new TaskResponseDto(userInformations, Task.Nome, Task.Status, Task.Data);
                return TaskResponse;
            }

            return null;
        }

        public async Task<bool> DeleteTask(Guid id, CancellationToken token)
        {
            await _repository.DeleteTask(id, token);
            return true;
        }
    }
}
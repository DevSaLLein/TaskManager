using TaskManager.DTO;
using TaskManager.Interface;
using TaskManager.Model;

namespace TaskManager.Service
{
    public class TaskService(ITaskRepository repository) : ITaskService
    {
        private readonly ITaskRepository _repository = repository;

        public async Task<Guid> CreateTask(TaskRequestDto dto, CancellationToken token)
        {
            var Task = await _repository.CreateTask(dto, token);
            return Task.Id;
        }

        public async Task<bool> UpdateTask(Guid id, TaskRequestDto dto, CancellationToken token)
        {
            TaskItem Task = await _repository.UpdateTask(dto, id, token);

            if(Task != null) 
            {
                return true;
            }

            return false;
        }

        public async Task<List<TaskResponseDto>> GetAllTasks(CancellationToken token)
        {
            List<TaskItem> Tasks = await _repository.GetAllTasks(token);

            List<TaskResponseDto> ListOfTasksResponse = new List<TaskResponseDto>();

            foreach(TaskItem TaskItem in Tasks)
            {
                TaskResponseDto TaskReponse = new TaskResponseDto(TaskItem.Nome, TaskItem.Status, TaskItem.Data, TaskItem.IdUser);

                ListOfTasksResponse.Add(TaskReponse);
            }

            return ListOfTasksResponse;
        }

        public async Task<TaskResponseDto> GetOneTask(Guid id, CancellationToken token)
        {
            TaskItem Task = await _repository.GetOneTask(id, token);
            
            if(Task != null)
            {
                TaskResponseDto TaskResponse = new TaskResponseDto(Task.Nome, Task.Status, Task.Data, Task.IdUser);
                return TaskResponse;
            }

            return null;
        }

        public async Task<bool> DeleteTask(Guid id, CancellationToken token)
        {
            TaskItem Task = await _repository.GetOneTask(id, token);

            if(Task != null) 
            {
                await _repository.DeleteTask(id, token);
                return true;
            }

            return false;
        }

        public async Task<UserResponseDto> GetAllTasksByUserResponse(Guid idUser, CancellationToken token)
        {
            UsuárioModel TasksByUser = await _repository.GetTaskItemsByUser(idUser, token);

            UserResponseDto UserWithYoursTasksResponse = new UserResponseDto(
                TasksByUser.Id, 
                TasksByUser.Login, 
                TasksByUser.Tasks
            );  

            return UserWithYoursTasksResponse;
        }
    }
}
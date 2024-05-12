using TaskManager.Helpers;
using TaskManager.Interfaces;
using TaskManager.Model;
using TaskManager.DTO;
using TasManager.DTO.Response.User;
using TasManager.Models;

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

        public async Task<List<TaskResponseDto>> GetAllTasks(QueryObjectFilter Filter, CancellationToken token)
        {
            List<UserTasks> Tasks = await _repository.GetAllTasks(Filter, token);

            List<TaskResponseDto> ListOfTasksResponse = new List<TaskResponseDto>();

            foreach(UserTasks TaskItem in Tasks)
            {
                var User = TaskItem.User;
                UserInformationsToTasksDto userInformations = new UserInformationsToTasksDto (User.UserName, User.Email);

                TaskResponseDto TaskReponse = new TaskResponseDto(userInformations, TaskItem.Task.Nome, TaskItem.Task.Status, TaskItem.Task.Data);

                ListOfTasksResponse.Add(TaskReponse);
            }

            return ListOfTasksResponse;
        }

        public async Task<TaskResponseDto> GetOneTask(Guid id, CancellationToken token)
        {
            TaskItem Task = await _repository.GetOneTask(id, token);

            var User = Task.UserTasks.Select(Entity => Entity.User).FirstOrDefault();
            UserInformationsToTasksDto userInformations = new UserInformationsToTasksDto (User.UserName, User.Email);
            
            if(Task != null)
            {
                TaskResponseDto TaskResponse = new TaskResponseDto(userInformations, Task.Nome, Task.Status, Task.Data);
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
    }
}
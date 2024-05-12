using TaskManager.Helpers;
using TaskManager.Interfaces;
using TaskManager.Model;
using TaskManager.DTO;

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
            List<TaskItem> Tasks = await _repository.GetAllTasks(Filter, token);

            List<TaskResponseDto> ListOfTasksResponse = new List<TaskResponseDto>();

            foreach(TaskItem TaskItem in Tasks)
            {
                TaskResponseDto TaskReponse = new TaskResponseDto(TaskItem.Nome, TaskItem.Status, TaskItem.Data);

                ListOfTasksResponse.Add(TaskReponse);
            }

            return ListOfTasksResponse;
        }

        public async Task<TaskResponseDto> GetOneTask(Guid id, CancellationToken token)
        {
            TaskItem Task = await _repository.GetOneTask(id, token);
            
            if(Task != null)
            {
                TaskResponseDto TaskResponse = new TaskResponseDto(Task.Nome, Task.Status, Task.Data);
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
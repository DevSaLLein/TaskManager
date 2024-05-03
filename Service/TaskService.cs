using TaskManager.DTO;
using TaskManager.Interface;
using TaskManager.Model;

namespace TaskManager.Service
{
    public class TaskService(ITaskRepository repository) : ITaskService
    {
        private readonly ITaskRepository _repository = repository;

        public async Task<bool> CreateTask(TaskRequestDto dto, CancellationToken token)
        {
            TaskItem? task = await _repository.GetOneTaskByPhone(dto.Telefone, token);

            if(task == null) 
            {
                await _repository.CreateTask(dto, token);
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateTask(Guid id, TaskRequestDto dto, CancellationToken token)
        {
            TaskItem? task = await _repository.UpdateTask(dto, id, token);

            if(task != null) 
            {
                return true;
            }

            return false;
        }

        public async Task<List<TaskResponseDto>> GetAllTasks(CancellationToken token)
        {
            List<TaskItem> tasks = await _repository.GetAllTasks(token);

            List<TaskResponseDto> listResponse = new List<TaskResponseDto>();

            foreach(TaskItem taskItem in tasks)
            {
                TaskResponseDto taskReponse = new TaskResponseDto(taskItem.Nome, taskItem.Telefone, taskItem.Status, taskItem.Data);

                listResponse.Add(taskReponse);
            }

            return listResponse;
        }

        public async Task<TaskResponseDto> GetOneTask(Guid id, CancellationToken token)
        {
            TaskItem? task = await _repository.GetOneTask(id, token);
            
            if(task != null)
            {
                TaskResponseDto taskDto = new TaskResponseDto(task.Nome, task.Telefone, task.Status, task.Data);
                return taskDto;
            }

            return null;
        }

        public async Task<bool> DeleteTask(Guid id, CancellationToken token)
        {
            TaskItem? task = await _repository.GetOneTask(id, token);

            if(task != null) 
            {
                await _repository.DeleteTask(id, token);
                return true;
            }

            return false;
        }
    }
}
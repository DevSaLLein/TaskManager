using Microsoft.AspNetCore.Mvc;
using TaskManager.DTO;
using TaskManager.Interface;
using TaskManager.Model;

namespace TaskManager.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController(ITask repository) : ControllerBase
    {
        private readonly ITask _repository = repository;

        [HttpPost]
        public async Task<ActionResult> CreateTask([FromBody] TaskRequestDto dto, CancellationToken token)
        {
            TaskItem task = await _repository.CreateTask(dto, token);

            return Ok(task);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllTasks(CancellationToken token)
        {
            List<TaskItem> tasks =  await _repository.GetAllTasks(token);

            return Ok(tasks);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetoneTask(Guid id, CancellationToken token)
        {
            TaskItem? taskSelectedById = await _repository.GetOneTask(id, token);

            return Ok(taskSelectedById);
        }

        [HttpPatch("{id:guid}")]
        public async Task<ActionResult> UpdateTask(Guid id, [FromBody] TaskRequestDto dto, CancellationToken token)
        {
            TaskItem? task = await _repository.UpdateTask(dto, id, token);

            return Ok(task);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteTask(Guid id, CancellationToken token)
        {
            TaskItem? task = await _repository.DeleteTask(id, token);
            
            return Ok(task);
        }
    }
}
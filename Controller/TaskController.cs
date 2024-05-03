using Microsoft.AspNetCore.Mvc;
using TaskManager.DTO;
using TaskManager.Interface;
using TaskManager.Model;

namespace TaskManager.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController(ITaskService service) : ControllerBase
    {
        private readonly ITaskService _service = service;

        [HttpPost]
        public async Task<ActionResult> CreateTask([FromBody] TaskRequestDto dto, CancellationToken token)
        {
            await _service.CreateTask(dto, token);

            return Created();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllTasks(CancellationToken token)
        {
            List<TaskResponseDto> tasks =  await _service.GetAllTasks(token);

            return Ok(tasks);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetoneTask(Guid id, CancellationToken token)
        {
            TaskResponseDto? taskSelectedById = await _service.GetOneTask(id, token);

            return Ok(taskSelectedById);
        }

        [HttpPatch("{id:guid}")]
        public async Task<ActionResult> UpdateTask(Guid id, [FromBody] TaskRequestDto dto, CancellationToken token)
        {
            bool taskIsFound = await _service.UpdateTask(id, dto,  token);
            
            return Ok(taskIsFound);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteTask(Guid id, CancellationToken token)
        {
            await _service.DeleteTask(id, token);
            return NoContent();
        }
    }
}
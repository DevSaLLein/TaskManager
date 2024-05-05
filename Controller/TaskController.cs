using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.DTO;
using TaskManager.Interface;

namespace TaskManager.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController(ITaskService service) : ControllerBase
    {
        private readonly ITaskService _service = service;

        [HttpPost]
        public async Task<ActionResult> CreateTask([FromBody] TaskRequestDto dto, CancellationToken token)
        {
            bool success = await _service.CreateTask(dto, token);

            if(success) return Created();

            return BadRequest("Error ao criar uma nova tarefa");
        }

        [HttpGet]
        public async Task<ActionResult> GetAllTasks(CancellationToken token)
        {
            List<TaskResponseDto> tasks =  await _service.GetAllTasks(token);

            return Ok(tasks);
        }

        [HttpGet("/porUser/{id:guid}")]
        public async Task<ActionResult> GetAllTasksByUser(Guid id, CancellationToken token)
        {
            var tasks = await _service.GetAllTasksByUserResponse(id, token);

            return Ok(tasks);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetoneTask(Guid id, CancellationToken token)
        {
            TaskResponseDto? taskSelectedById = await _service.GetOneTask(id, token);

            if(taskSelectedById != null) return Ok(taskSelectedById);

            return NotFound("Tarefa não encontrada");
        }

        [HttpPatch("{id:guid}")]
        public async Task<ActionResult> UpdateTask(Guid id, [FromBody] TaskRequestDto dto, CancellationToken token)
        {
            bool taskIsFound = await _service.UpdateTask(id, dto,  token);

            if(taskIsFound) return Ok(taskIsFound);
            
            return NotFound("Tarefa não encontrada");
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteTask(Guid id, CancellationToken token)
        {
            bool taskIsFound = await _service.DeleteTask(id, token);

            if(taskIsFound) return NoContent();

            return NotFound("Tarefa não encontrada");
        }
    }
}
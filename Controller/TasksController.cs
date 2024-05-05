using Microsoft.AspNetCore.Mvc;
using TaskManager.DTO;
using TaskManager.Interface;

namespace TaskManager.Controller
{
    // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController(ITaskService service) : ControllerBase
    {
        private readonly ITaskService _service = service;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateTask([FromBody] TaskRequestDto dto, CancellationToken token)
        {
            var guidFromTask = await _service.CreateTask(dto, token);

            return CreatedAtAction(nameof(GetOneTask), new { id = guidFromTask }, "Task created");
        }

        [HttpGet]
        public async Task<ActionResult> GetAllTasks(CancellationToken token)
        {
            List<TaskResponseDto> tasks =  await _service.GetAllTasks(token);

            return Ok(tasks);
        }

        [HttpGet("/byUser/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAllTasksByUser([FromRoute] Guid id, CancellationToken token)
        {
            var tasks = await _service.GetAllTasksByUserResponse(id, token);

            return Ok(tasks);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetOneTask([FromRoute] Guid id, CancellationToken token)
        {
            TaskResponseDto? taskSelectedById = await _service.GetOneTask(id, token);

            if(taskSelectedById != null) return Ok(taskSelectedById);

            return NotFound("Tarefa não encontrada");
        }

        [HttpPatch("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status304NotModified)]
        public async Task<ActionResult> UpdateTask([FromRoute] Guid id, [FromBody] TaskRequestDto dto, CancellationToken token)
        {
            bool taskIsFound = await _service.UpdateTask(id, dto,  token);

            if(taskIsFound) return NoContent();
            
            return NotFound("Tarefa não encontrada");
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteTask([FromRoute] Guid id, CancellationToken token)
        {
            bool taskIsFound = await _service.DeleteTask(id, token);

            if(taskIsFound) return NoContent();

            return NotFound("Tarefa não encontrada");
        }
    }
}
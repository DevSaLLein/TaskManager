using Microsoft.AspNetCore.Mvc;
using TaskManager.DTO;
using TaskManager.Interface;

namespace TaskManager.Controller
{
    // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController(ITaskService Service) : ControllerBase
    {
        private readonly ITaskService _service = Service;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateTask([FromBody] TaskRequestDto Dto, CancellationToken Token)
        {
            var GuidFromTask = await _service.CreateTask(Dto, Token);

            return CreatedAtAction(nameof(GetOneTask), new { Id = GuidFromTask }, "Task created");
        }

        [HttpGet]
        public async Task<ActionResult> GetAllTasks(CancellationToken Token)
        {
            List<TaskResponseDto> Tasks =  await _service.GetAllTasks(Token);

            return Ok(Tasks);
        }

        [HttpGet("/byUser/{Id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAllTasksByUser([FromRoute] Guid Id, CancellationToken Token)
        {
            var TasksByUser = await _service.GetAllTasksByUserResponse(Id, Token);

            return Ok(TasksByUser);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetOneTask([FromRoute] Guid Id, CancellationToken Token)
        {
            TaskResponseDto TaskSelectedById = await _service.GetOneTask(Id, Token);

            if(TaskSelectedById != null) return Ok(TaskSelectedById);

            return NotFound("Tarefa não encontrada");
        }

        [HttpPatch("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status304NotModified)]
        public async Task<ActionResult> UpdateTask([FromRoute] Guid Id, [FromBody] TaskRequestDto Dto, CancellationToken Token)
        {
            bool TaskIsFound = await _service.UpdateTask(Id, Dto, Token);

            if(TaskIsFound) return NoContent();
            
            return NotFound("Tarefa não encontrada");
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteTask([FromRoute] Guid Id, CancellationToken Token)
        {
            bool TaskIsFound = await _service.DeleteTask(Id, Token);

            if(TaskIsFound) return NoContent();

            return NotFound("Tarefa não encontrada");
        }
    }
}
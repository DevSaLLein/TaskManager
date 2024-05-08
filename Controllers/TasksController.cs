using Microsoft.AspNetCore.Mvc;    
using TaskManager.Helpers;
using TaskManager.Interfaces;
using TaskManager.DTO;

namespace TaskManager.Controller
    {
        // [Authorize]
        [ApiController]
        [Route("api/[controller]")]
        public class TasksController(ITaskService Service) : ControllerBase
        {
            private readonly ITaskService _service = Service;

            [HttpPost]
            public async Task<ActionResult> CreateTask([FromBody] TaskCreateRequestDto Dto, CancellationToken Token)
            {
                var GuidFromTask = await _service.CreateTask(Dto, Token);

                return CreatedAtAction(nameof(GetOneTask), new { Id = GuidFromTask }, Created());
            }

            [HttpGet]
            public async Task<ActionResult> GetAllTasks([FromQuery] QueryObjectFilter Filter, CancellationToken Token)
            {
                List<TaskResponseDto> Tasks =  await _service.GetAllTasks(Filter, Token);

                return Ok(Tasks);
            }

            // [HttpGet("/byUser/{Id:guid}")]
            // public async Task<ActionResult> GetAllTasksByUser([FromQuery] QueryObjectFilter Filter, [FromRoute] Guid Id, CancellationToken Token)
            // {
            //     var TasksByUser = await _service.GetAllTasksByUserResponse(Filter, Id, Token);

            //     return Ok(TasksByUser);
            // }

            [HttpGet("{Id:guid}")]
            public async Task<ActionResult> GetOneTask([FromRoute] Guid Id, CancellationToken Token)
            {
                TaskResponseDto TaskSelectedById = await _service.GetOneTask(Id, Token);

                if(TaskSelectedById != null) return Ok(TaskSelectedById);

                return NotFound("Tarefa não encontrada");
            }

            [HttpPut("{Id:guid}")]
            public async Task<ActionResult> UpdateTask([FromRoute] Guid Id, [FromBody] TaskUpdateRequestDto Dto, CancellationToken Token)
            {
                if(!ModelState.IsValid) return BadRequest(ModelState);

                bool TaskIsFound = await _service.UpdateTask(Id, Dto, Token);

                if(TaskIsFound) return NoContent();
                
                return NotFound("Tarefa não encontrada");
            }

            [HttpDelete("{Id:guid}")]        
            public async Task<ActionResult> DeleteTask([FromRoute] Guid Id, CancellationToken Token)
            {
                bool TaskIsFound = await _service.DeleteTask(Id, Token);

                if(TaskIsFound) return NoContent();

                return NotFound("Tarefa não encontrada");
            }
        }
    }
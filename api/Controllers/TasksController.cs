// using Microsoft.AspNetCore.Mvc;    
// using TaskManager.Helpers;
// using TaskManager.DTO;
// using TasManager.Extensions;

// namespace TaskManager.Controller
//     {
//         // [Authorize]
//         [ApiController]
//         [Route("api/[controller]")]
//         public class TasksController(ITaskService Service) : ControllerBase
//         {
//             private readonly ITaskService _service = Service;

//             [HttpPost]
//             public async Task<ActionResult> CreateTask([FromBody] TaskCreateRequestDto Dto, CancellationToken Token)
//             {
//                 var Username = User.GetUsername();

//                 var IdFromTask = await _service.CreateTask(Dto, Username, Token);

//                 return CreatedAtAction(nameof(GetOneTask), new { Id =IdFromTask }, Ok("Task Created"));
//             }

//             [HttpGet]
//             public async Task<ActionResult> GetAllTasks([FromQuery] QueryObjectFilter Filter, CancellationToken Token)
//             {
//                 var Tasks =  await _service.GetAllTasks(Filter, Token);

//                 return Ok(Tasks);
//             }


//             [HttpGet("{Id}")]
//             public async Task<ActionResult> GetOneTask([FromRoute] Guid Id, CancellationToken Token)
//             {
//                 TaskResponseDto TaskSelectedById = await _service.GetOneTask(Id, Token);

//                 if(TaskSelectedById == null)  return NotFound("Tarefa não encontrada");

//                 return Ok(TaskSelectedById);
//             }

//             [HttpPut("{Id}")]
//             public async Task<ActionResult> UpdateTask([FromRoute] Guid Id, [FromBody] TaskUpdateRequestDto Dto, CancellationToken Token)
//             {
//                 if(!ModelState.IsValid) return BadRequest(ModelState);

//                 bool TaskIsFound = await _service.UpdateTask(Id, Dto, Token);

//                 if(TaskIsFound) return NoContent();
                
//                 return NotFound("Tarefa não encontrada");
//             }

//             [HttpDelete("{Id}")]        
//             public async Task<ActionResult> DeleteTask([FromRoute] Guid Id, CancellationToken Token)
//             {
//                 var Username = User.GetUsername();

//                 bool TaskIsFound = await _service.DeleteTask(Id, Token);

//                 if(TaskIsFound) return NoContent();

//                 return NotFound("Tarefa não encontrada");
//             }
//         }
//     }
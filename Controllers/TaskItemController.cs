using Microsoft.AspNetCore.Mvc;
using TaskManager.Context;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskItemController(TaskManagerContext taskManagerContext) : ControllerBase
    {
        private readonly TaskManagerContext _context = taskManagerContext;

        [HttpPost]
        public IActionResult CreateNewTask(TaskItem task)
        {
            if(_context.Tasks.Find(task.Id) != null) return Conflict("Task already created");

            if (task.Data == DateTime.MinValue) 
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" })
            ;

            _context.Add(task);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTaskById), new {id = task.Id}, task);
        }

        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            if(_context.Tasks.Find(id) == null) return NotFound("Task not found");

            TaskItem task = _context.Tasks.Find(id);
            return Ok(task);
        }

        [HttpGet("ObterTodos")]
        public IActionResult GetAllTasks()
        {
            return Ok(_context.Tasks.ToList());
        }

        [HttpGet("ObterPorTitulo/{title}")]
        public IActionResult GetAllTasksByTitle(string title)
        {
            var task = _context.Tasks.Where(task => task.Titulo.Contains(title));

            return Ok(task);
        }

        [HttpGet("ObterPorStatus/{status}")]
        public IActionResult GetAllTasksByStatus(int status)
        {

            return Ok(status);
        }

        [HttpGet("ObterPorData/{date}")]
        public IActionResult GetAllTasksByDate(DateTime date)
        {
            var task = _context.Tasks.Where(task => task.Data.Equals(date));

            return Ok(task);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, TaskItem task)
        {
            
            if(_context.Tasks.Find(id) == null) return NotFound("Task not found");

            if (task.Data == DateTime.MinValue) 
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" })
            ;

            TaskItem taskFromDB = _context.Tasks.Find(id);

            taskFromDB.Titulo = task.Titulo;
            taskFromDB.Descricao  = task.Descricao;
            taskFromDB.Data = task.Data;
            taskFromDB.Status = task.Status;

            _context.Tasks.Update(taskFromDB);
            _context.SaveChanges();

            return Ok(taskFromDB);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            if(_context.Tasks.Find(id) == null) return NotFound("Task not found");

            _context.Tasks.Remove(_context.Tasks.Find(id));
            _context.SaveChanges();

            return NoContent();
        }
    }
}
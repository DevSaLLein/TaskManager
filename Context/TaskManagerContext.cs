
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Context
{
    public class TaskManagerContext(DbContextOptions<TaskManagerContext> options) : DbContext(options)
    { 
        public DbSet<TaskItem> Tasks {get; set;}
    }
}
using Microsoft.EntityFrameworkCore;
using TaskManager.Context.Map;
using TaskManager.Model;

namespace TaskManager.Context
{
    public class TaskManagerContext(DbContextOptions<TaskManagerContext> Option) : DbContext(Option)
    {
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<LocationModel> Localizações { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
        {
            OptionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            
            base.OnConfiguring(OptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
            ModelBuilder.ApplyConfiguration(new TaskMap());
            ModelBuilder.ApplyConfiguration(new UserMap());
            ModelBuilder.ApplyConfiguration(new LocationMap());

            base.OnModelCreating(ModelBuilder);
        }
    }
}
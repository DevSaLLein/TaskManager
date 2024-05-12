using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Context.Map;
using TaskManager.Model;
using TasManager.Context.Map;
using TasManager.Models;

namespace TaskManager.Context
{
    public class TaskManagerContext(DbContextOptions<TaskManagerContext> Option) : IdentityDbContext<UserIdentityApp>(Option)
    {
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<LocationModel> Location { get; set; }
        public DbSet<UserTasks> UserTasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
        {
            OptionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            
            base.OnConfiguring(OptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
            ModelBuilder.ApplyConfiguration(new TaskMap());
            ModelBuilder.ApplyConfiguration(new LocationMap());    
            ModelBuilder.ApplyConfiguration(new UserTasksMap()); 
            
            List<IdentityRole> Roles = new List<IdentityRole>
            {
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "User", NormalizedName = "USER" }
            };

            ModelBuilder.Entity<IdentityRole>().HasData(Roles);

            base.OnModelCreating(ModelBuilder);
        }
    }
}
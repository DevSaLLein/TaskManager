using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TasManager.Context.Map
{
    public class UserTasksMap : IEntityTypeConfiguration<UserTasks>
    {
        public void Configure(EntityTypeBuilder<UserTasks> Builder)
        {
            Builder.HasKey(Entity => new { Entity.UserId, Entity.TaskId });

            Builder
                .HasOne(Entity => Entity.User)
                .WithMany(Entity => Entity.UserTasks)
                .HasForeignKey(Entity => Entity.UserId)
            ;  
            
            Builder
                .HasOne(Entity => Entity.Task)
                .WithMany(Entity => Entity.UserTasks)
                .HasForeignKey(Entity => Entity.TaskId)
            ;               
        }
    }
}
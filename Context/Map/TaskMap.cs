using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Model;

namespace TaskManager.Context.Map
{
    public class TaskMap : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Nome).IsRequired().HasMaxLength(255);
            
            builder.HasOne(task => task.Login)
            .WithMany(login => login.Tasks)
            .HasForeignKey(task => task.IdLogin);
        }
    }
}
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Model;

namespace TaskManager.Context.Map
{
    public class TaskMap : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> Builder)
        {
            Builder.HasKey(entity => entity.Id);
            Builder.Property(entity => entity.Nome).IsRequired();
        }
    }
}
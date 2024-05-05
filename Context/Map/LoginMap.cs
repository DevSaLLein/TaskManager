using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Model;

namespace TaskManager.Context.Map
{
    
    public class LoginMap : IEntityTypeConfiguration<UsuárioModel>
    {
        public void Configure(EntityTypeBuilder<UsuárioModel> Builder)
        {
            Builder.HasKey(entity => entity.Id);
            Builder.Property(entity => entity.Login).IsRequired().HasMaxLength(255);
            Builder.Property(entity => entity.Senha).IsRequired().HasMaxLength(20);
            Builder.Property(entity => entity.Token).IsRequired();  
        }      
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Model;

namespace TaskManager.Context.Map
{
    
    public class LoginMap : IEntityTypeConfiguration<UsuárioModel>
    {

        public void Configure(EntityTypeBuilder<UsuárioModel> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Login).IsRequired().HasMaxLength(255);
            builder.Property(entity => entity.Senha).IsRequired().HasMaxLength(20);
            builder.Property(entity => entity.Token).IsRequired();  
        }      
    }
}
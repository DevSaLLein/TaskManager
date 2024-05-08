using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Model;

namespace TaskManager.Context.Map
{
    
    public class UserMap : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> Builder)
        {
            Builder.HasKey(entity => entity.Id);
            Builder.Property(entity => entity.Login).IsRequired().HasMaxLength(255);
            Builder.Property(entity => entity.Senha).IsRequired().HasMaxLength(20);
            Builder.Property(entity => entity.JwtAuthentication).IsRequired();  

            Builder
                .HasOne(entity => entity.Location)
                .WithMany(entity => entity.Usuários)
                .HasForeignKey(entity => entity.Cep)
            ;
        }      
    }
}
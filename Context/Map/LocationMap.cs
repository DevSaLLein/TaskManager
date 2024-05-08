using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Model;

namespace TaskManager.Context.Map
{
    public class LocationMap : IEntityTypeConfiguration<LocationModel>
    {
        public void Configure(EntityTypeBuilder<LocationModel> Builder)
        {
            Builder.HasKey(entity => entity.Cep);
            Builder.Property(entity => entity.Logradouro).IsRequired();        
            Builder.Property(entity => entity.Complemento).IsRequired();
            Builder.Property(entity => entity.Bairro).IsRequired();
            Builder.Property(entity => entity.Localidade).IsRequired();        
            Builder.Property(entity => entity.Ddd).IsRequired();
            Builder.Property(entity => entity.Uf).IsRequired();        
        }
    }
}
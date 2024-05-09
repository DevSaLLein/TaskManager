using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TasManager.Models;

namespace TasManager.Context.Map
{
    public class UserIdentityMap : IEntityTypeConfiguration<UserIdentityApp>
    {
        public void Configure(EntityTypeBuilder<UserIdentityApp> Builder)
        {
            List<IdentityRole> Roles = new List<IdentityRole>
            {
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "User", NormalizedName = "USER" }
            };

            Builder.HasData(Roles);
        }
    }
}
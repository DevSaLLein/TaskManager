using Microsoft.AspNetCore.Identity;

namespace TasManager.Models
{
    public class UserIdentityApp : IdentityUser
    {
        public string Cep { get; set; }

        public List<UserTasks> UserTasks { get; set; }
    }
}
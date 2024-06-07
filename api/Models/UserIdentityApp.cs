namespace TasManager.Models
{
    public class UserIdentityApp : IdentityUser
    {
        public string Cep { get; set; }

        [JsonIgnore]
        public List<UserTasks> UserTasks { get; set; }
    }
}
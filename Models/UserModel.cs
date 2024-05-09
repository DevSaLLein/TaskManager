using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TaskManager.Model
{
    public class UserModel(string login, string senha, string cep, string JwtAuthentication)
    {
        public Guid Id { get; init; }
        
        public string Login { get; set; } = login;

        public string Senha { get; set; } = senha;

        public string JwtAuthentication { get; set; } = JwtAuthentication;

        public virtual ICollection<TaskItem> Tasks { get; set; }

        [ForeignKey("Cep")]
        [JsonIgnore]
        public string Cep { get; set; } = cep; 

        [JsonIgnore]
        public virtual LocationModel Location { get; set; }
    }
}


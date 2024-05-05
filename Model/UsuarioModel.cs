using System.ComponentModel.DataAnnotations;

namespace TaskManager.Model
{
    public class UsuárioModel
    {
        public Guid Id { get; init; }

        [Required]
        public string Login { get; set; } = string.Empty;

        [Required]
        public string Senha { get; set; } = string.Empty;
        
        public string Token { get; set; } = string.Empty;

        public virtual ICollection<TaskItem> Tasks { get; set; }

        public UsuárioModel(string login, string senha, string token)
        {
            Login = login;
            Senha = senha;
            Token = token;
        }
    }
}

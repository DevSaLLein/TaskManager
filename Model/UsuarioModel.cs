using System.ComponentModel.DataAnnotations;

namespace TaskManager.Model
{
    public class UsuárioModel(string login, string senha, string token)
    {
        public Guid Id { get; init; }

        [Required]
        public string Login { get; set; } = login;

        [Required]
        public string Senha { get; set; } = senha;

        public string Token { get; set; } = token;

        public virtual ICollection<TaskItem> Tasks { get; set; }
    }
}

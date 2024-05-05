using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Enum;

namespace TaskManager.Model
{
    public class TaskItem
    {
        public Guid Id { get; init; }

        [Required]
        public string Nome { get; private set; } = string.Empty;

        [Required]
        public StatusEnum Status { get; private set; } = StatusEnum.Pendente;
        public DateTime Data { get; private set; } = DateTime.UtcNow;

        [Required]
        public Guid IdLogin { get; set; }

        [ForeignKey("IdLogin")]
        public virtual LoginModel Login { get; set; }

        public TaskItem(string nome, Guid idLogin)
        {
            Nome = nome;
            IdLogin = idLogin;
        }

        public void UpdateTask(string nome)
        {
            Nome = nome;
        }
    }
}

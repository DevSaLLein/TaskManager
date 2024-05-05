using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TaskManager.Enum;

namespace TaskManager.Model
{
    public class TaskItem(string nome, Guid idUser)
    {
        public Guid Id { get; init; }

        public string Nome { get; private set; } = nome;

        public StatusEnum Status { get; private set; } = StatusEnum.Pendente;

        public DateTime Data { get; private set; } = DateTime.UtcNow;

        public Guid IdUser { get; set; } = idUser;

        [ForeignKey("IdUser")]
        [JsonIgnore]
        public virtual UsuárioModel Usuario { get; set; }

        public void UpdateTask(string nome)
        {
            Nome = nome;
        }
    }
}

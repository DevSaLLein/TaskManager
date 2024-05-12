using Newtonsoft.Json;
using TaskManager.Enum;
using TasManager.Models;

namespace TaskManager.Model
{
    public class TaskItem
    {
        public Guid Id { get; init; }

        public string Nome { get; set; }

        public StatusEnum Status { get; set; } = StatusEnum.Pendente;

        public DateTime Data { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public virtual List<UserTasks> UserTasks { get; set; } = new List<UserTasks>(); 

        public TaskItem(string nome)
        {
            Nome = nome;
        }

        public TaskItem(Guid id, string nome, StatusEnum status, DateTime data)
        {
            Id = id;
            Nome = nome;
            Status = status;
            Data = data;
        }
    }
}

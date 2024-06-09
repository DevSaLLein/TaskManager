using TaskManager.Enum;

namespace TaskManager.Model
{
    public class TaskItem
    {
        public Guid Id { get; init; }

        public string Nome { get; set; }

        public StatusEnum Status { get; set; } = StatusEnum.Pendente;

        public DateTime Data { get; set; } = DateTime.UtcNow;

        
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual List<UserTasks> UserTasks { get; set; } 

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

        public TaskItem(UserTasks userTasks)
        {
            Id = userTasks.TaskId;
            Nome = userTasks.Task.Nome;
            Status = userTasks.Task.Status;
            Data = userTasks.Task.Data;
        }
    }
}

using Newtonsoft.Json;
using TaskManager.Enum;

namespace TaskManager.Model
{
    public class TaskItem(string nome, Guid idUser)
    {
        public Guid Id { get; init; }

        public string Nome { get; private set; } = nome;

        public StatusEnum Status { get; private set; } = StatusEnum.Pendente;

        public DateTime Data { get; private set; } = DateTime.UtcNow;

        [JsonIgnore]
        public Guid IdUser { get; set; } = idUser;

        [JsonIgnore]
        public virtual UsuárioModel Usuario { get; set; }

        public void UpdateTask(string nome)
        {
            Nome = nome;
        }
    }
}

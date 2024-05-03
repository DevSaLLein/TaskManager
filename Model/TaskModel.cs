using TaskManager.Enum;

namespace TaskManager.Model
{
    public class TaskItem(string nome, string telefone)
    {
        public Guid Id { get; init; }
        public string Nome { get; private set; } = nome;
        public string Telefone { get; private set; } = telefone;
        public StatusEnum Status { get; private set; } = StatusEnum.Pendente;
        public DateTime Data { get; private set; } = DateTime.UtcNow;

        public void UpdateTask(string nome, string telefone)
        {
            Nome = nome;
            Telefone = telefone;
        }
    }
}
using TaskManager.Enum;

namespace api.DTO.Tarefa.Response
{
    public class TarefaResponse()
    {
        public Guid Id {get; set; } 
        public string Nome {get; set; } 
        public StatusEnum Status {get; set; } 
        public DateTime Data {get; set; }
    }
}
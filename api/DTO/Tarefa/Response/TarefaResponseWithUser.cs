using TaskManager.Enum;
using TasManager.DTO.Account.Response;

namespace TaskManager.DTO.Tarefa.Response
{
    public class TarefaResponseWithUser : UserInformationsToTarefas
    {

        public TarefaResponseWithUser
        (
            UserInformationsToTarefas user,
            string nome,
            StatusEnum status,
            DateTime date

        ): base(user.UserName, user.Email)
        {
            Nome = nome;
            Status = status;
            Date = date;
        }

        public string Nome { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime Date { get; set; }
    }
}
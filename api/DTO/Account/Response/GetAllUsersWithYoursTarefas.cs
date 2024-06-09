using TaskManager.Model;

namespace TasManager.DTO.Account.Response
{
    public class GetAllUsersWithYoursTarefas
    (
        UserInformationsToTarefas user,
        List<TaskItem> tasks
    )
    {
        public UserInformationsToTarefas User  { get; set; } = user;
        public List<TaskItem> TaskItems { get; set; } = tasks;   
    }
}
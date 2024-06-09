namespace TasManager.DTO.Account.Response
{
    public class UserInformationsToTarefas
    (
        string username,
        string email
    )
    {
        public string UserName { get; set; } = username;
        public string Email { get; set; } = email;
    }
}
namespace TasManager.DTO.Response.User
{
    public class UserInformationsToTasksDto
    (
        string username,
        string email,
        string cep
    )
    {
        public string UserName { get; set; } = username;
        public string Email { get; set; } = email;
        public string Cep { get; set; } = cep;
    }
}
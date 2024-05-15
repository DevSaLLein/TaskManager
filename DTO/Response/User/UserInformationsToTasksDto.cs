namespace TasManager.DTO.Response.User
{
    public class UserInformationsToTasksDto
    (
        string username,
        string email  
    )
    {
        public string UserName { get; set; } = username;
        public string Email { get; set; } = email;
    }
}
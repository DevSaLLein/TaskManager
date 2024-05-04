
namespace TaskManager.Model
{
    public class LoginModel(string login, string senha, string token)
    {
        public Guid Id { get; init; }
        public string? Login { get; set; } = login;
        public string? Senha { get; set; } = senha;
        public string? Token { get; set; } = token;
    }
}
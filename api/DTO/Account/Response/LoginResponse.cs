namespace TasManager.DTO.Account.Response
{
    public record LoginResponse
     (
        string UserName,
        string Email,
        string Token
    )
    { }
}
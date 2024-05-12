namespace TasManager.DTO.Response
{
    public record UserLoginDto
     (
        string UserName,
        string Email,
        string Token
    )
    { }
}
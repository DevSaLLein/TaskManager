namespace TasManager.DTO.Response
{
    public record UserCreatedDto
    (
        string UserName,
        string Email,
        string Token
    )
    {}
}
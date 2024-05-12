using TaskManager.DTO;

namespace TasManager.DTO.Response
{
    public record UserCreatedDto
    (
        string UserName,
        string Email,
        ViaCepResponse ViaCepResponse,
        string Token
    )
    {}
}
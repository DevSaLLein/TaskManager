using TaskManager.DTO;

namespace TasManager.DTO.Account.Response
{
    public record RegisterResponse
    (
        string UserName,
        string Email,
        ViaCepResponse ViaCepResponse,
        string Token
    )
    {}
}
using System.ComponentModel.DataAnnotations;

namespace TasManager.DTO.Account.Request
{
    public record LoginRequest
    (
        [Required]
        string UserName,

        [Required]
        string Password
    )
    {}
}
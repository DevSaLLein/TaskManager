using System.ComponentModel.DataAnnotations;

namespace TasManager.DTO.Request
{
    public record RegisterDto
    (
        [Required]
        string UserName,

        [Required]
        [EmailAddress]
        string Email,

        [Required]
        string Password
    )
    {}
}
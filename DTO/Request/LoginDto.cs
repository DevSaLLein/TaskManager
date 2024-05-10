using System.ComponentModel.DataAnnotations;

namespace TasManager.DTO.Request
{
    public record LoginDto
    (
        [Required]
        string UserName,

        [Required]
        string Password
    )
    {}
}
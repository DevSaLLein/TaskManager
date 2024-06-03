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
        string Password,

        [Required]
        [MinLength(8, ErrorMessage = "Cep have 8 caracteres")]
        [MaxLength(8, ErrorMessage = "Cep have 8 caracteres")]
        string Cep
    )
    {}
}
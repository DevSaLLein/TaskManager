using System.ComponentModel.DataAnnotations;

namespace TasManager.DTO.Account.Request
{
    public record RegisterRequest
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
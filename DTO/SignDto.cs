using System.ComponentModel.DataAnnotations;

namespace TaskManager.DTO
{
    public record SignDto(

        [Required]
        [MinLength(5, ErrorMessage = "Deve ter no mínimo 5 caracteres")]
        string Login, 

        [Required]
        [MinLength(8, ErrorMessage = "Deve ter no mínimo 8 caracteres")]
        string Password
    ) { }
}
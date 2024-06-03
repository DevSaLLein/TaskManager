using System.ComponentModel.DataAnnotations;

namespace TaskManager.DTO
{
    public record TaskCreateRequestDto
    (
        [Required]
        [MinLength(8, ErrorMessage = "Deve ter no mínimo 8 caracteres")]
        string Nome
    ) { }
}
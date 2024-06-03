using System.ComponentModel.DataAnnotations;

namespace TaskManager.DTO
{
    public record TaskUpdateRequestDto
    (
        [Required]
        [MinLength(8, ErrorMessage = "Deve ter no mínimo 8 caracteres")]
        string Nome
    )
    { }
}
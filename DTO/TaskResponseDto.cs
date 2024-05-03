using TaskManager.Enum;

namespace TaskManager.DTO
{
    public record TaskResponseDto
    (
        string Nome, 
        string Telefone,
        StatusEnum Status,
        DateTime Data
    ){ }
}
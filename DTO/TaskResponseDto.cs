using TaskManager.Enum;
namespace TaskManager.DTO
{
    public record TaskResponseDto
    (
        string Nome, 
        StatusEnum Status,
        DateTime Data,
        Guid IdUser
    ){ }
}
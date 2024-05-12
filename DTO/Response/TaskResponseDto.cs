using TaskManager.Enum;
using TasManager.DTO.Response.User;
using TasManager.Models;

namespace TaskManager.DTO
{
    public record TaskResponseDto
    (
        UserInformationsToTasksDto User,
        string Nome,
        StatusEnum Status,
        DateTime Data
    ){}
}
using System.Runtime.InteropServices;
using TaskManager.Enum;
using TaskManager.Model;

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
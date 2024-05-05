using TaskManager.Model;

namespace TaskManager.DTO
{
    public record UserResponseDto(Guid IdUser, string Login, ICollection<TaskItem> Tasks) {}
}
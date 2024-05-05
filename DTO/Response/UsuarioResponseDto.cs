using TaskManager.Model;

namespace TaskManager.DTO
{
    public record UsuarioResponseDto
    (
        Guid IdUser, 
        string Login, 
        ICollection<TaskItem> Tasks
    ) { }
}
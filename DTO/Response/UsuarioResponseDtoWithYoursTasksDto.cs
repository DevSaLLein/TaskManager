using TaskManager.Model;

namespace TaskManager.DTO
{
    public record UsuarioResponseDtoWithYoursTasks
    (
        Guid IdUser, 
        string Login, 
        LocationModel Location,
        ICollection<TaskItem> Tasks
    ) { }
}
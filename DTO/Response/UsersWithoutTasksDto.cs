using TaskManager.Model;

namespace TaskManager.DTO.Response
{
    public record UsersWithoutTasksDto
    (
        Guid IdUser, 
        string Login, 
        LocationModel Location
    )
    { }
}
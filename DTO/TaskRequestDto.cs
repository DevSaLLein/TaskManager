namespace TaskManager.DTO
{
    public record TaskRequestDto
    (
        string Nome, 
        Guid IdUser
    ) { }
}
using TaskManager.Enum;

namespace TaskManager.DTO
{
    public record TaskOrdedByUserResponse
    (
        string NomeDaTarefa, 
        StatusEnum Status, 
        DateTime Data, 
        string User    
    )
    {
        
    }
}
namespace TaskManager.DTO
{
    public record TaskRequestDto(string Nome, string Telefone, Enum.StatusEnum Status)
    {
        
    }
}
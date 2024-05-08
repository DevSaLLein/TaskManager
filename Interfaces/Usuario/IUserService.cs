using TaskManager.DTO;
using TaskManager.DTO.Response;
using TaskManager.Helpers;

namespace TaskManager.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUser(UserCreateRequestDto dto, string JwtAuthentication, CancellationToken Token);

        Task<UsuarioResponseDtoWithYoursTasks> GetAllUsersWithYoursTasksResponse(QueryObjectFilter Filter, Guid IdUser, CancellationToken Token);
        
        Task<List<UsuarioResponseDtoWithYoursTasks>> GetAllUsersWithYoursTasksAndLocalizationDetails(QueryObjectFilter Filter, CancellationToken Token);   

        // Task<UsersWithoutTasksDto> GetUserByLogin(string Login, CancellationToken Token);
    }
}
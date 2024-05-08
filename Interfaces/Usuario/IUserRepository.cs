using TaskManager.DTO;
using TaskManager.Helpers;
using TaskManager.Model;

namespace TaskManager.Interfaces
{
    public interface IUserRepository
    {
        Task<UserModel> CreateUser(UserCreateRequestDto dto, string JwtAuthentication, CancellationToken Token);

        Task<LocationModel> CreateLocalizacao(ViaCepResponse ViaCepResponse, CancellationToken Token);

        Task<List<UserModel>> GetAllUsersWithoutTasksAndLocalization(QueryObjectFilter Filter, CancellationToken Token);

        Task<List<UserModel>> GetAllUsersWithYoursTasksAndLocalizationDetails(QueryObjectFilter Filter, CancellationToken Token);   

        Task<UserModel> GetUserByLogin(string Login, CancellationToken Token);
    }
}
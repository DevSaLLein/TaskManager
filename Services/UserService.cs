using TaskManager.DTO;
using TaskManager.Helpers;
using TaskManager.Interfaces;
using TaskManager.Model;

namespace TaskManager.Service
{
    public class UserService(IUserRepository Repository) : IUserService
    {
        private readonly IUserRepository _repository = Repository;

        public async Task<bool> CreateUser(UserCreateRequestDto Dto, string JwtAuthentication,CancellationToken Token)
        {
            var User = await _repository.CreateUser(Dto, JwtAuthentication, Token);

            if(User != null) return true;
            else return false;
        }

        public async Task<List<UsuarioResponseDtoWithYoursTasks>> GetAllUsersWithYoursTasksAndLocalizationDetails(QueryObjectFilter Filter, CancellationToken token)
        {
            List<UserModel> TasksByUser = await _repository.GetAllUsersWithYoursTasksAndLocalizationDetails(Filter, token);

            if(TasksByUser == null)
            {
                throw new Exception("Não há tasks para esse usuário nesse status");
            }

            List<UsuarioResponseDtoWithYoursTasks> UserWithYoursTasksResponse = new List<UsuarioResponseDtoWithYoursTasks>();

            foreach(UserModel User in TasksByUser)
            {
                UsuarioResponseDtoWithYoursTasks UserResponse = new UsuarioResponseDtoWithYoursTasks
                (
                    User.Id, 
                    User.Login,
                    User.Location, 
                    User.Tasks                    
                );
                
                UserWithYoursTasksResponse.Add (UserResponse);  
            }

            return UserWithYoursTasksResponse;
        }

        public Task<UsuarioResponseDtoWithYoursTasks> GetAllUsersWithYoursTasksResponse(QueryObjectFilter Filter, Guid IdUser, CancellationToken Token)
        {
            return null;
        }   
    }
}
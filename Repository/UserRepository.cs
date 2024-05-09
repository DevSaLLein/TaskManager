using ConsumoDeAPIs.Integration.Interfaces;
using Microsoft.EntityFrameworkCore;
using TaskManager.Context;
using TaskManager.DTO;
using TaskManager.Helpers;
using TaskManager.Interfaces;
using TaskManager.Model;

namespace TaskManager.Repository
{
    public class UserRepository(TaskManagerContext Database, IViaCepIntegracao ViaCep) : IUserRepository
    {
        private readonly TaskManagerContext _database = Database;
        private readonly IViaCepIntegracao _cep = ViaCep;

        public async Task<LocationModel> CreateLocalizacao(ViaCepResponse ViaCepResponse, CancellationToken Token)
        {
            var cepAlreadyExist = await _database.Location.SingleOrDefaultAsync(Entity => Entity.Cep.Contains(ViaCepResponse.Cep), cancellationToken: Token);

            if(cepAlreadyExist != null) return cepAlreadyExist;

            LocationModel Localizacao = new LocationModel
            (
                ViaCepResponse.Cep, 
                ViaCepResponse.Logradouro, 
                ViaCepResponse.Complemento, 
                ViaCepResponse.Bairro, 
                ViaCepResponse.Localidade, 
                ViaCepResponse.Uf, 
                ViaCepResponse.Ddd
            );

            await _database.Location.AddAsync(Localizacao, Token);
            await _database.SaveChangesAsync(Token);

            return cepAlreadyExist;
        }

        public async Task<UserModel> CreateUser(UserCreateRequestDto dto, string JwtAuthentication, CancellationToken Token)
        {

            var isCepValid = await _cep.ObterDadosViaCep(dto.Cep);
            UserModel isUserExist = await GetUserByLogin(dto.Login, Token);

            if(isCepValid == null && isUserExist != null) return null;

            await CreateLocalizacao(isCepValid, Token);

            UserModel NewUser = new UserModel(dto.Login, dto.Password, dto.Cep, JwtAuthentication);

            await _database.AddAsync(NewUser, Token);

            await _database.SaveChangesAsync(Token);

            return isUserExist;
        }

        public async Task<List<UserModel>> GetAllUsersWithoutTasksAndLocalization(QueryObjectFilter Filter, CancellationToken Token)
        {
            return await _database.UsersSign.ToListAsync(Token);
        }

        public async Task<List<UserModel>> GetAllUsersWithYoursTasksAndLocalizationDetails(QueryObjectFilter Filter, CancellationToken Token)
        {
            var Query = _database.UsersSign
                .Include(Entity => Entity.Tasks)
                .Include(Entity => Entity.Location)
                .AsQueryable();
            ;

            if(Filter.Status != null){
                Query.Where(Entity => Entity.Tasks.Any(Task => Task.Status == Filter.Status));
            }

            var SkipNumber = (Filter.PageNumber - 1) * Filter.PageSize;
            
            var UsersWithYoursTasksAndLocalizationDetails = 
                await Query
                    .Skip(SkipNumber)
                    .Take(Filter.PageSize)
                    .ToListAsync(Token)
                ;
            ; 

            return UsersWithYoursTasksAndLocalizationDetails;
        }

        public async Task<UserModel> GetUserByLogin(string Login, CancellationToken Token)
        {
            return await _database.UsersSign.SingleOrDefaultAsync(Entity => Entity.Login == Login, cancellationToken: Token);
        }
    }
}
using TasManager.Models;

namespace TasManager.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(UserIdentityApp user);
    }
}
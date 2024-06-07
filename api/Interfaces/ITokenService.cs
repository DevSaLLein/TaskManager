namespace TasManager.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(UserIdentityApp user);
    }
}
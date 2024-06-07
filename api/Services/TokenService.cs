using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TasManager.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration Config)
        {
            _config = Config;   
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        }

        public string CreateToken(UserIdentityApp User)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, User.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, User.UserName)
            };

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials,
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
            }; 

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        } 
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskManager.DTO;
using TaskManager.Helpers;
using TaskManager.Interfaces;
using TaskManager.Model;

namespace TaskManager.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IUserService Service, IConfiguration Configuration, UserManager<UserModel> UserManager) : ControllerBase
    {
        private readonly IConfiguration _configuration = Configuration;        
        private readonly IUserService _service = Service;

        private readonly UserManager<UserModel> _userManager = UserManager;

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] UserCreateRequestDto Dto, CancellationToken Token)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var JwtAuthentication = GenerateTokenJWT(Dto.Login);
            await _service.CreateUser(Dto, JwtAuthentication, Token);

            return Ok(new { Mensage = "Usuário cadastrado com sucesso", JwtAuthentication });          
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUsersWithYoursTasksAndLocalizationDetails([FromQuery] QueryObjectFilter Filter, CancellationToken Token)
        {
            List<UsuarioResponseDtoWithYoursTasks> UsersWithTasksAndLocalization = await _service.GetAllUsersWithYoursTasksAndLocalizationDetails(Filter, Token);

            return Ok(UsersWithTasksAndLocalization);
        }

        private string GenerateTokenJWT(string Login)
        {
            string Key = _configuration["Jwt:Key"];
            string Issuer = _configuration["Jwt:Issuer"];
            string Audience = _configuration["Jwt:Audience"];

            SymmetricSecurityKey KeyCrip = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            SigningCredentials Credential = new SigningCredentials(KeyCrip, SecurityAlgorithms.HmacSha256);

            var Claims = new[]
            {
                new Claim(ClaimTypes.Name, Login),
            };

            var Token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: Claims, 
                expires: DateTime.Now.AddHours(2),
                signingCredentials: Credential
            );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}

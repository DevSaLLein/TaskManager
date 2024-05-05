using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Context;
using TaskManager.DTO;
using TaskManager.Model;

namespace TaskManager.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController(TaskManagerContext Database, IConfiguration Configuration) : ControllerBase
    {
        private readonly IConfiguration _configuration = Configuration;        
        private readonly TaskManagerContext _database = Database;

        [HttpPost]
        public IActionResult SignUp([FromBody] SignDto Dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var Token = GenerateTokenJWT(Dto.Login);

                var LoginAlreadyExist = _database.Usuarios.Where(Entity => Entity.Login == Dto.Login);

                if (!LoginAlreadyExist.IsNullOrEmpty()) return Conflict("Usuário já cadastrado");

                UsuárioModel NewLogin = new UsuárioModel(Dto.Login, Dto.Password, Token);

                _database.Usuarios.Add(NewLogin);
                _database.SaveChanges();

                return Ok(new { Mensage = "Usuário cadastrado com sucesso", Token });
            }
            catch (Exception Ex)
            {
                Console.WriteLine($"Erro ao tentar logar: {Ex.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
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

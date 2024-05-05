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
    public class UsuariosController(TaskManagerContext database, IConfiguration configuration) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;        
        private readonly TaskManagerContext _database = database;

        [HttpPost]
        public IActionResult SignUp([FromBody] SignDto dto)
        {
            try
            {
                var loginAlreadyExist = _database.Usuarios.Where(login => login.Login == dto.Login);

                if(!loginAlreadyExist.IsNullOrEmpty()) return Conflict("Usuário já cadastrado");

                var token = GenerateTokenJWT(dto.Login);

                UsuárioModel newLogin = new UsuárioModel(dto.Login, dto.Password, token);
                
                _database.Usuarios.Add(newLogin);
                _database.SaveChanges();

                return Ok(new { mensage = "Usuário cadastrado com sucesso", token });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar logar: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        private string GenerateTokenJWT(string login)
        {
            string key = _configuration["Jwt:Key"];

            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credential = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, login),
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims, 
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credential
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

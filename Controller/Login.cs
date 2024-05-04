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
    public class LoginController(TaskManagerContext database) : ControllerBase
    {
        private readonly string key = "0cbd2ce1-1f06-49f4-a693-0ae2f767db85";

        private readonly TaskManagerContext _database = database;

        [HttpPost]
        public IActionResult Sign([FromBody] SignDto dto)
        {
            try
            {
                var loginAlreadyExist = _database.Login.Where(login => login.Login == dto.Login);

                if(!loginAlreadyExist.IsNullOrEmpty()) return Conflict("Usuário já cadastrado");

                var token = GenerateTokenJWT(dto.Login);

                LoginModel newLogin = new LoginModel(dto.Login, dto.Password, token);
                
                _database.Login.Add(newLogin);
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
            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credential = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("login", login),
            };

            var token = new JwtSecurityToken(
                issuer: "",
                audience: "",
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credential
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

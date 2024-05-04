using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Model;

namespace TaskManager.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly string key = "0cbd2ce1-1f06-49f4-a693-0ae2f767db85";

        [HttpPost]
        public IActionResult Logar([FromBody] LoginModel login)
        {
            try
            {
                if (login.Login == "admin" && login.Senha == "admin")
                {
                    var token = GenerateTokenJWT();
                    return Ok(new { token });
                }

                return BadRequest(new { mensage = "Credenciais inválidas" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar logar: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        private string GenerateTokenJWT()
        {
            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credential = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("login", "admin"),
                new Claim("email", "admin@gmail.com")
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

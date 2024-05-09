using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasManager.DTO.Request;
using TasManager.DTO.Response;
using TasManager.Interfaces;
using TasManager.Models;

namespace TasManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController(UserManager<UserIdentityApp> User, ITokenService Token) : ControllerBase
    {

        private readonly UserManager<UserIdentityApp> _user = User;
        private readonly ITokenService _token = Token;

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            try 
            {
                if(!ModelState.IsValid) return BadRequest(ModelState);

                var User = new UserIdentityApp
                {
                    UserName = register.UserName,
                    Email = register.Email,
                };

                var createUser = await _user.CreateAsync(User, register.Password);

                if(createUser.Succeeded)
                {
                    var roleResult = await _user.AddToRoleAsync(User, "User");

                    if(roleResult.Succeeded) 
                    {
                        var Token = _token.CreateToken(User);

                        UserCreatedDto UserDto = new(register.UserName, register.Email, Token);

                        return Ok(UserDto);
                    }

                    else return StatusCode(500, roleResult.Errors);
                }

                else return StatusCode(500, createUser.Errors);
            }
            catch(Exception error)
            {
                return StatusCode(500, error);
            }
        }
    }
}
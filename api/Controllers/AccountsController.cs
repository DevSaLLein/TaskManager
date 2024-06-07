using Microsoft.AspNetCore.Mvc;
using TasManager.DTO.Request;
using TasManager.DTO.Response;

namespace TasManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController(UserManager<UserIdentityApp> User, SignInManager<UserIdentityApp> signInManager, IViaCepIntegracao viaCep, ITokenService Token) : ControllerBase
    {

        private readonly UserManager<UserIdentityApp> _user = User;
        private readonly SignInManager<UserIdentityApp> _signIn = signInManager;
        private readonly ITokenService _token = Token;
        private readonly IViaCepIntegracao _viaCep = viaCep;

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            try 
            {
                if(!ModelState.IsValid) return BadRequest(ModelState);

                var ResultViaCep = await _viaCep.ObterDadosViaCep(register.Cep);

                if(ResultViaCep == null) return NotFound("Cep not found");

                var User = new UserIdentityApp
                {
                    UserName = register.UserName,
                    Email = register.Email,
                    Cep = ResultViaCep.Cep
                };

                var createUser = await _user.CreateAsync(User, register.Password);

                if(createUser.Succeeded)
                {
                    var roleResult = await _user.AddToRoleAsync(User, "User");

                    if(roleResult.Succeeded) 
                    {
                        var Token = _token.CreateToken(User);

                        UserCreatedDto UserDto = new(register.UserName, register.Email, ResultViaCep, Token);

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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto, CancellationToken Token)
        {   
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var User = await _user.Users.FirstOrDefaultAsync(Entity => Entity.UserName == loginDto.UserName, cancellationToken: Token);

            if(User == null) return NotFound("Invalid UserName");

            var result = await _signIn.CheckPasswordSignInAsync(User, loginDto.Password, Token.CanBeCanceled);

            if(!result.Succeeded) return Unauthorized("Invalid password");

            UserLoginDto UserDto = new(User.UserName, User.Email, _token.CreateToken(User));

            return Ok(UserDto);
        }
    }
}
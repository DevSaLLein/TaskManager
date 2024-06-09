
// using Microsoft.AspNetCore.Mvc;
// using TasManager.Extensions;

// namespace TasManager.Controllers
// {
//     // [Authorize]
//     [ApiController]
//     [Route("[controller]")]
//     public class UsersController(UserManager<UserIdentityApp> UserIdentity, IUserRepository Repository) : ControllerBase
//     {
//         private readonly UserManager<UserIdentityApp> _user = UserIdentity;
//         private readonly IUserRepository _repository = Repository;

//         [HttpGet("GetAllTasksByUser")]
//         public async Task<IActionResult> GetAllTasksFromOneUser()
//         {
//             var username = User.GetUsername();

//             var UserEntity = await _user.FindByNameAsync(username);

//             var UserWithYoursTasks = await _repository.getAllTasks(UserEntity); 

//             return Ok(UserWithYoursTasks);
//         }
//     }
// }
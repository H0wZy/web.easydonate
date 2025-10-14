using Microsoft.AspNetCore.Mvc;
using User.Api.Models;
using User.Api.Repositories.UserRepository;

namespace User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserRepository userInterface) : ControllerBase
    {
        [HttpGet("GetAllAsync")]
        public async Task<ActionResult<RespondeModel<List<UserModel>>>> GetAllUsersAsync()
        {
            var users = await userInterface.GetAllUsersAsync();
            return Ok(users);
        }
    }
}
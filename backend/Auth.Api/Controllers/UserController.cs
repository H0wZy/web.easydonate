using Auth.Api.Models;
using Auth.Api.Models.Response;
using Auth.Api.Repositories.UserRepository;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserRepository userInterface) : ControllerBase
    {
        [HttpGet("GetAllAsync")]
        public async Task<ActionResult<ApiResponse<List<UserModel>>>> GetAllUsersAsync()
        {
            var users = await userInterface.GetAllUsersAsync();
            return Ok(users);
        }
    }
}
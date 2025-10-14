using Microsoft.AspNetCore.Mvc;
using User.Api.Models;
using User.Api.Repositories.UserRepository;
using User.Api.Services.UserService;

namespace User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpGet("GetAllUsersAsync")]
        public async Task<ActionResult<RespondeModel<List<UserModel>>>> GetAllUsersAsync()
        {
            var response = await userService.GetAllUsersAsync();
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
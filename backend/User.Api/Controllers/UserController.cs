using Microsoft.AspNetCore.Mvc;
using User.Api.Dto;
using User.Api.Models;
using User.Api.Services.UserService;

namespace User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpGet("GetAllUsersAsync")]
        public async Task<ActionResult<ResponseModel<List<UserModel>>>> GetAllUsersAsync()
        {
            var response = await userService.GetAllUsersAsync();
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetUserByIdAsync")]
        public async Task<ActionResult<ResponseModel<UserModel>>> GetUserByIdAsync(int id)
        {
            var response = await userService.GetUserByIdAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost("CreateUserAsync")]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDto dto)
        {
            var response = await userService.CreateUserAsync(dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPatch("UpdateUserAsync/{id:int}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UpdateUserDto dto)
        {
            var response = await userService.UpdateUserAsync(id, dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("DeleteUserAsync")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var response = await userService.DeleteUserAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
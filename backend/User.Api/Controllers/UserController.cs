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
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<ResponseModel<List<UserModel>>>> GetAllUsers()
        {
            var response = await userService.GetAllUsersAsync();
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetUserById")]
        public async Task<ActionResult<ResponseModel<UserModel>>> GetUserById(int id)
        {
            var response = await userService.GetUserByIdAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<ResponseModel<UserModel>>> CreateUser([FromBody] CreateUserDto dto)
        {
            var response = await userService.CreateUserAsync(dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPatch("UpdateUserById/{id:int}")]
        public async Task<ActionResult<ResponseModel<UserModel>>> UpdateUserById(int id, [FromBody] UpdateUserDto dto)
        {
            var response = await userService.UpdateUserByIdAsync(id, dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("DeleteUserById/{id:int}")]
        public async Task<ActionResult<ResponseModel<object>>> DeleteUserById(int id)
        {
            var response = await userService.DeleteUserByIdAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
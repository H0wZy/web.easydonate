using Auth.Api.Dto;
using Auth.Api.Model;
using Auth.Api.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            // A lógica de autenticação virá aqui
            return Ok("Login recebido com sucesso.");
        }

        [HttpGet("test-user/{email}")]
        public async Task<ActionResult<ResponseModel<UserDto>>> AuthenticateAsync(string email, string username, string password)
        {
            var loginDto = new LoginDto ("username", email, "password");
            var response = await authService.AuthenticateAsync(loginDto);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}

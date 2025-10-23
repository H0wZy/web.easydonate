using Auth.Api.Dto;
using Auth.Api.Model;
using Auth.Api.Services.AuthService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<ResponseModel<TokenResponseDto>>> Login([FromBody] LoginDto loginDto)
        {
            var response = await authService.AuthenticateAsync(loginDto);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}

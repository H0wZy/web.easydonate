using Auth.Api.Dto;
using Auth.Api.Model;

namespace Auth.Api.Services.AuthService
{
    public interface IAuthService
    {
        public Task<ResponseModel<LoginResponseDto>> AuthenticateAsync(LoginDto dto);
    }
}
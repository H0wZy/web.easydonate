using Auth.Api.Dto;
using Auth.Api.Model;
using System.Threading.Tasks;

namespace Auth.Api.Services.AuthService
{
    public interface IAuthService
    {
        public Task<ResponseModel<TokenResponseDto>> AuthenticateAsync(LoginDto dto);
    }
}
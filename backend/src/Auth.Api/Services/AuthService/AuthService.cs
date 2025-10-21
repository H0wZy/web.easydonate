using Auth.Api.Clients;
using Auth.Api.Dto;
using Auth.Api.Model;

namespace Auth.Api.Services.AuthService
{
    public class AuthService(IUserApiClient userApiClient) : IAuthService
    {
        public async Task<ResponseModel<LoginResponseDto>> AuthenticateAsync(LoginDto dto)
        {
            var user = await userApiClient.GetUserByEmailAsync(dto.Email);
            // A implementação virá aqui
            throw new NotImplementedException();
        }
    }
}
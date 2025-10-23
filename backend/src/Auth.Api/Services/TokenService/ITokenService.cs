using Auth.Api.Dto;

namespace Auth.Api.Services.TokenService
{
    public interface ITokenService
    {
        string GenerateToken(UserDto user);
    }
}
using Auth.Api.Dto;

namespace Auth.Api.Services.TokenService
{
    public interface ITokenService
    {
        public TokenResponseDto GenerateToken(UserDto user);
    }
}
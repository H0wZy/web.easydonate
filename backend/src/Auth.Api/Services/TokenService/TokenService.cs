using Auth.Api.Dto;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Api.Services.TokenService
{
    public class TokenService(IConfiguration configuration) : ITokenService
    {
        public TokenResponseDto GenerateToken(UserDto user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserType.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            var key = configuration["JWT:Key"] ?? throw new InvalidOperationException("JWT Key não configurada");
            var issuer = configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT Issuer não configurado");
            var audience = configuration["Jwt:Audience"] ?? throw new InvalidOperationException("JWT Audience não configurado");

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var expiresAt = DateTime.UtcNow.AddHours(8);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiresAt,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return new TokenResponseDto
            {
                AccessToken = accessToken,
                ExpiresAt = expiresAt
            };
        }
    }
}
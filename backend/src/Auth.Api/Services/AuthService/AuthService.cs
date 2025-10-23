using Auth.Api.Clients;
using Auth.Api.Dto;
using Auth.Api.Model;
using Auth.Api.Services.TokenService;
using Auth.Api.Utils;
using System;
using System.Threading.Tasks;

namespace Auth.Api.Services.AuthService
{
    public class AuthService(IUserApiClient userApiClient, ITokenService tokenService) : IAuthService
    {
        public async Task<ResponseModel<TokenResponseDto>> AuthenticateAsync(LoginDto dto)
        {
            try
            {
                UserDto? user;

                if (dto.Login.Contains('@'))
                {
                    user = await userApiClient.GetUserByEmailAsync(dto.Login);
                }
                else
                {
                    user = await userApiClient.GetUserByUsernameAsync(dto.Login);
                }

                if (user is null || user.IsUserDisabled)
                {
                    return ResponseModel<TokenResponseDto>.Fail("Credenciais inválidas.");
                }

                var savedHash = Convert.FromBase64String(user.HashPassword);
                var savedSalt = Convert.FromBase64String(user.SaltPassword);

                var isPasswordValid = PasswordHelper.VerifyPassword(dto.Password, savedHash, savedSalt);

                if (!isPasswordValid)
                {
                    return ResponseModel<TokenResponseDto>.Fail("Credenciais inválidas.");
                }

                var tokenResponse = tokenService.GenerateToken(user);
                
                //TODO: PEDIR AO USER.API ATUALIZAR LastLoginAt.
                
                return ResponseModel<TokenResponseDto>.Ok(tokenResponse, "Usuário autenticado com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DEBUG: Erro na autenticação: {ex.Message}");
                return ResponseModel<TokenResponseDto>.Fail($"Erro interno: {ex.Message}");
            }
        }
    }
}
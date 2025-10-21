using Auth.Api.Clients;
using Auth.Api.Dto;
using Auth.Api.Model;

namespace Auth.Api.Services.AuthService
{
    public class AuthService(IUserApiClient userApiClient) : IAuthService
    {
        public async Task<ResponseModel<UserDto>> AuthenticateAsync(LoginDto dto)
        {
            try
            {
                Console.WriteLine($"DEBUG: Buscando usuário com email: {dto.Email}");
                
                var user = await userApiClient.GetUserByEmailAsync(dto.Email);
                
                if (user == null)
                {
                    Console.WriteLine($"DEBUG: Usuário não encontrado");
                    return ResponseModel<UserDto>.Fail("Usuário não encontrado.");
                }

                Console.WriteLine($"DEBUG: Usuário encontrado: {user.Username}");
                
                // TODO: Aqui você vai implementar a validação de senha
                // var isPasswordValid = VerifyPassword(dto.Password, user.HashPassword, user.SaltPassword);
                
                return ResponseModel<UserDto>.Ok(user, "Usuário autenticado com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DEBUG: Erro na autenticação: {ex.Message}");
                return ResponseModel<UserDto>.Fail($"Erro interno: {ex.Message}");
            }
        }
    }
}
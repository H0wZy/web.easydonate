using User.Api.Models;
using User.Api.Repositories.UserRepository;

namespace User.Api.Services.UserService;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<RespondeModel<List<UserModel>>> GetAllUsersAsync()
    {
        try
        {
            var users = await userRepository.GetAllUsersAsync();

            return new RespondeModel<List<UserModel>>
            {
                Data = users,
                Success = true,
                Message = "Usuário(s) encontrado(s) com sucesso."
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new RespondeModel<List<UserModel>>
            {
                Data = null,
                Success = false,
                Message = ex.Message
            };
        }
    }
}
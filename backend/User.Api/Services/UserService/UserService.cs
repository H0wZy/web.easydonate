using System.Linq.Expressions;
using User.Api.Dto;
using User.Api.Models;
using User.Api.Repositories.UserRepository;

namespace User.Api.Services.UserService;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<ResponseModel<List<UserModel>>> GetAllUsersAsync()
    {
        try
        {
            var users = await userRepository.GetAllUsersAsync();

            return ResponseModel<List<UserModel>>.Ok(users,
                users.Count == 0 ? "Nenhum usuário encontrado." : $"{users.Count} usuário(s) encontrado(s) com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return ResponseModel<List<UserModel>>.Fail($"Erro interno: {ex.Message}.");
        }
    }

    public Task<ResponseModel<UserModel>> GetUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<UserModel>> GetUserAsync(Expression<Func<UserModel?, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseModel<UserModel>> CreateUserAsync(CreateUserDto dto)
    {
        try
        {
            var existingData =
                await userRepository.GetUserAsync(u =>
                    u.Email == dto.Email || u.Username == dto.Username);

            if (existingData is not null)
            {
                if (existingData.Email == dto.Email)
                    return ResponseModel<UserModel>.Fail("E-mail já cadastrado.");
                if (existingData.Username == dto.Username)
                    return ResponseModel<UserModel>.Fail("Nome de usuário já cadastrado.");
            }

            var user = new UserModel
            {
                Username = dto.Username,
                Email = dto.Email,
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                Password = dto.Password,
                ConfirmPassword = dto.ConfirmPassword,
                UserType = dto.UserType,
                IsUserDisabled = dto.IsUserDisabled,
                AcceptedTerms = dto.AcceptedTerms
            };

            var createdUser = await userRepository.CreateUserAsync(user);
            return ResponseModel<UserModel>.Ok(createdUser, "Usuário criado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return ResponseModel<UserModel>.Fail($"Erro interno: {ex.Message}");
        }
    }

    public Task<ResponseModel<UserModel>> UpdateUserAsync(UpdateUserDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseModel<UserModel>> DeleteUserAsync(int id)
    {
        try
        {
            var user = await userRepository.GetUserByIdAsync(id);

            if (user is null)
            {
                return ResponseModel<UserModel>.Fail("Usuário não encontrado.");
            }

            await userRepository.DeleteUserAsync(user);

            return ResponseModel<UserModel>.Ok(user, "Usuário deletado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return ResponseModel<UserModel>.Fail($"Erro ao deletar: {ex.Message}");
        }
    }
}
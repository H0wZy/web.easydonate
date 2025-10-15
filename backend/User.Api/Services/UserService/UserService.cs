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
                users.Count == 0
                    ? "Nenhum usuário encontrado."
                    : $"{users.Count} usuário(s) encontrado(s) com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return ResponseModel<List<UserModel>>.Fail($"Erro interno: {ex.Message}.");
        }
    }

    public async Task<ResponseModel<UserModel>> GetUserByIdAsync(int id)
    {
        try
        {
            var user = await userRepository.GetUserByIdAsync(id);

            return user is null
                ? ResponseModel<UserModel>.Fail($"Nenhum usuário encontrado com o ID {id}.")
                : ResponseModel<UserModel>.Ok(user, "Usuário encontrado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return ResponseModel<UserModel>.Fail($"Erro interno: {ex.Message}.");
        }
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

    public async Task<ResponseModel<UserModel>> UpdateUserAsync(UpdateUserDto dto)
    {
        try
        {
            var existingUser = await userRepository.GetUserByIdAsync(dto.Id);

            if (existingUser is null)
            {
                return ResponseModel<UserModel>.Fail($"Usuário com ID {dto.Id} não encontrado.");
            }

            var duplicateCheck =
                await userRepository.GetUserAsync(u =>
                    (u.Email == dto.Email || u.Username == dto.Username) && u.Id != dto.Id);

            if (duplicateCheck is not null)
            {
                if (duplicateCheck.Email == dto.Email)
                    return ResponseModel<UserModel>.Fail("Este e-mail já está sendo utilizado.");
                if (duplicateCheck.Username == dto.Username)
                    return ResponseModel<UserModel>.Fail("Este nome de usuário já está sendo utilizado.");
            }

            var nothingChanged = existingUser.Username == dto.Username
                                 && existingUser.Email == dto.Email
                                 && existingUser.Firstname == dto.Firstname
                                 && existingUser.Lastname == dto.Lastname;

            if (nothingChanged)
            {
                return ResponseModel<UserModel>.Fail("Nada foi alterado.");
            }

            existingUser.Username = dto.Username;
            existingUser.Email = dto.Email;
            existingUser.Firstname = dto.Firstname;
            existingUser.Lastname = dto.Lastname;
            existingUser.UpdatedAt = DateTime.UtcNow;

            var updatedUser = await userRepository.UpdateUserAsync(existingUser);

            return ResponseModel<UserModel>.Ok(updatedUser, "Usuário atualizado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return ResponseModel<UserModel>.Fail($"Erro interno: {ex.Message}");
        }
    }

    public async Task<ResponseModel<UserModel>> DeleteUserAsync(int id)
    {
        try
        {
            var user = await userRepository.GetUserByIdAsync(id);

            if (user is null)
            {
                return ResponseModel<UserModel>.Fail($"Nenhum usuário encontrado com o ID {id}.");
            }

            await userRepository.DeleteUserAsync(user);
            return ResponseModel<UserModel>.Ok(user, $"Usuário {id} deletado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return ResponseModel<UserModel>.Fail($"Erro ao deletar: {ex.Message}");
        }
    }
}
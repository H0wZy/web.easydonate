using AutoMapper;
using User.Api.Dto;
using User.Api.Models;
using User.Api.Repositories.UserRepository;

namespace User.Api.Services.UserService;

public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
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

    public async Task<ResponseModel<UserModel>> UpdateUserAsync(int id, UpdateUserDto dto)
    {
        try
        {
            var user = await userRepository.GetUserByIdAsync(id);

            if (user is null)
            {
                return ResponseModel<UserModel>.Fail("Nenhum usuário encontrado.");
            }

            if (dto.Username == null && dto.Email == null && dto.Firstname == null && dto.Lastname == null)
                return ResponseModel<UserModel>.Fail("Nenhum campo alterado.");

            mapper.Map(dto, user);

            await userRepository.UpdateUserAsync(user);

            return ResponseModel<UserModel>.Ok(user, "Usuário atualizado com sucesso.");
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
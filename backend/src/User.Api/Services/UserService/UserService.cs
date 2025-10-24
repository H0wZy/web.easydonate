using AutoMapper;
using User.Api.Dto;
using User.Api.Models;
using User.Api.Repositories.UserRepository;
using User.Api.Utils;

namespace User.Api.Services.UserService;

public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
{
    public async Task<ResponseModel<List<UserDto>?>> GetAllUsersAsync()
    {
        try
        {
            var users = await userRepository.GetAllUsersAsync();
            var usersDto = mapper.Map<List<UserDto>>(users);

            return ResponseModel<List<UserDto>>.Ok(usersDto,
                users.Count == 0
                    ? "Nenhum usuário encontrado."
                    : $"{users.Count} usuário(s) encontrado(s) com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return ResponseModel<List<UserDto>>.Fail($"Erro interno: {ex.Message}.");
        }
    }

    public async Task<ResponseModel<UserDto?>> GetUserByIdAsync(int id)
    {
        try
        {
            var user = await userRepository.GetUserByIdAsync(id);

            if (user is null)
                return ResponseModel<UserDto>.Fail($"Nenhum usuário encontrado com o ID {id}.");

            var userDto = mapper.Map<UserDto>(user);
            return ResponseModel<UserDto>.Ok(userDto, "Usuário encontrado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return ResponseModel<UserDto>.Fail($"Erro interno: {ex.Message}.");
        }
    }

    public async Task<ResponseModel<UserDto?>> GetUserByUsernameAsync(string username)
    {
        try
        {
            var user = await userRepository.GetUserAsync(u => u.Username == username);

            if (user is null)
                return ResponseModel<UserDto>.Fail("Usuário não encontrado.");

            var userDto = mapper.Map<UserDto>(user);
            return ResponseModel<UserDto>.Ok(userDto, "Usuário encontrado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return ResponseModel<UserDto>.Fail($"Erro interno: {ex.Message}.");
        }
    }

    public async Task<ResponseModel<UserDto?>> GetUserByEmailAsync(string email)
    {
        try
        {
            var user = await userRepository.GetUserAsync(u => u.Email == email);

            if (user is null)
                return ResponseModel<UserDto>.Fail("Usuário não encontrado.");

            var userDto = mapper.Map<UserDto>(user);
            return ResponseModel<UserDto>.Ok(userDto, "Usuário encontrado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return ResponseModel<UserDto>.Fail($"Erro interno: {ex.Message}.");
        }
    }

    public async Task<ResponseModel<UserDto?>> CreateUserAsync(CreateUserDto dto)
    {
        try
        {
            var existingData =
                await userRepository.GetUserAsync(u =>
                    u.Email == dto.Email || u.Username == dto.Username);

            if (existingData is not null)
            {
                if (existingData.Email == dto.Email)
                    return ResponseModel<UserDto>.Fail("E-mail já cadastrado.");
                if (existingData.Username == dto.Username)
                    return ResponseModel<UserDto>.Fail("Nome de usuário já cadastrado.");
            }

            var (hashPassword, saltPassword) = PasswordHelper.HashPassword(dto.Password);

            var user = new UserModel
            {
                Username = dto.Username,
                Email = dto.Email,
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                HashPassword = hashPassword,
                SaltPassword = saltPassword,
                UserType = dto.UserType
            };

            var createdUser = await userRepository.CreateUserAsync(user);
            var createdUserDto = mapper.Map<UserDto>(createdUser);
            return ResponseModel<UserDto>.Ok(createdUserDto, "Usuário criado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return ResponseModel<UserDto>.Fail($"Erro interno: {ex.Message}");
        }
    }

    public async Task<ResponseModel<UserDto?>> UpdateUserByIdAsync(int id, UpdateUserDto dto)
    {
        try
        {
            var user = await userRepository.GetUserByIdAsync(id);

            if (user is null)
            {
                return ResponseModel<UserDto>.Fail("Nenhum usuário encontrado.");
            }

            if (dto.Username == null && dto.Email == null && dto.Firstname == null && dto.Lastname == null)
                return ResponseModel<UserDto>.Fail("Nenhum campo alterado.");

            var duplicateCheck = await userRepository.GetUserAsync(u =>
                (u.Username == dto.Username ||
                 u.Email == dto.Email) &&
                u.Id != id);

            if (duplicateCheck is not null)
            {
                if (duplicateCheck.Email == dto.Email)
                    return ResponseModel<UserDto>.Fail("Este e-mail já está sendo utilizado.");
                if (duplicateCheck.Username == dto.Username)
                    return ResponseModel<UserDto>.Fail("Este nome de usuário já está sendo utilizado.");
            }

            mapper.Map(dto, user);
            user.UpdatedAt = DateTime.UtcNow;
            await userRepository.UpdateUserAsync(user);

            var updatedUserDto = mapper.Map<UserDto>(user);
            return ResponseModel<UserDto>.Ok(updatedUserDto, "Usuário atualizado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return ResponseModel<UserDto>.Fail($"Erro interno: {ex.Message}");
        }
    }

    public async Task<ResponseModel<UserDto?>> UpdateLastLoginAsync(int id)
    {
        try
        {
            var user = await userRepository.GetUserByIdAsync(id);

            if (user is null)
                return ResponseModel<UserDto?>.Fail("Usuário não encontrado.");

            user.LastLoginAt = DateTime.UtcNow;
            await userRepository.UpdateUserAsync(user);
            
            var userDto = mapper.Map<UserDto>(user);
            return ResponseModel<UserDto>.Ok(userDto, "Data de último login atualizada com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return ResponseModel<UserDto>.Fail($"Erro ao atualizar data de último login: {ex.Message}");
        }
    }

    public async Task<ResponseModel<object?>> DeleteUserByIdAsync(int id)
    {
        try
        {
            var user = await userRepository.GetUserByIdAsync(id);

            if (user is null)
            {
                return ResponseModel<object>.Fail($"Nenhum usuário encontrado com o ID {id}.");
            }

            await userRepository.DeleteUserAsync(user);

            return ResponseModel<object>.Ok(null, $"Usuário {id} deletado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return ResponseModel<object>.Fail($"Erro ao deletar: {ex.Message}");
        }
    }

    public async Task<ResponseModel<UserDto?>> DisableUserByIdAsync(int id)
    {
        try
        {
            var user = await userRepository.GetUserByIdAsync(id);

            if (user is null)
            {
                return ResponseModel<UserDto>.Fail("Usuário não encontrado.");
            }

            user.IsUserDisabled = true;
            user.UpdatedAt = DateTime.UtcNow;
            await userRepository.UpdateUserAsync(user);

            var disabledUserDto = mapper.Map<UserDto>(user);
            return ResponseModel<UserDto>.Ok(disabledUserDto, "Usuário desativado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return ResponseModel<UserDto>.Fail($"Erro ao desativar: {ex.Message}");
        }
    }
}
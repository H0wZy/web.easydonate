using System.Linq.Expressions;
using Auth.Api.Data;
using Auth.Api.Dto;
using Auth.Api.Models;
using Auth.Api.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace Auth.Api.Repositories.UserRepository;

public class Util(AuthDbContext context) : IUserRepository
{
    public async Task<ApiResponse<UserModel>> GetByIdAsync(int id)
    {
        ApiResponse<UserModel> response = new();
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(user => user.UserId == id);

            if (user == null)
            {
                response.Success = false;
                response.Message = "Nenhum usuário encontrado com o ID informado.";
                return response;
            }

            response.Data = user;
            response.Message = "Usuário encontrado com sucesso!";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            Console.WriteLine(ex);
        }

        return response;
    }

    public async Task<ApiResponse<List<UserModel?>>> GetAllAsync()
    {
        ApiResponse<List<UserModel?>> response = new();

        try
        {
            var users = await context.Users.ToListAsync();

            response.Data = users;
            response.Success = true;
            response.Message = "Usuários encontrados com sucesso!";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            Console.WriteLine(ex.Message);
        }

        return response;
    }

    public async Task<ApiResponse<List<UserModel?>>> CreateUserAsync(CreateUserDto createUserDto)
    {
        ApiResponse<List<UserModel?>> response = new();

        try
        {
            var user = new UserModel
            {
                Username = createUserDto.Username,
                Email = createUserDto.Email,
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                Password = createUserDto.Password,
                UserType = createUserDto.UserType
            };

            context.Add(user);
            await context.SaveChangesAsync();

            response.Data = await context.Users.ToListAsync();
            response.Success = true;
            response.Message = "Usuário criado com sucesso!";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            Console.WriteLine(ex);
        }

        return response;
    }

    public async Task<ApiResponse<List<UserModel?>>> UpdateUserAsync(UpdateUserDto updateUserDto)
    {
        ApiResponse<List<UserModel?>> response = new();
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(user => user.UserId == updateUserDto.UserId);

            if (user is null)
            {
                response.Success = false;
                response.Message = "Usuário não encontrado.";
                return response;
            }

            user.Username = updateUserDto.Username;
            user.Email = updateUserDto.Email;
            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;

            context.Update(user);
            await context.SaveChangesAsync();

            response.Data = await context.Users.ToListAsync();
            response.Success = true;
            response.Message = "Usuário atualizado com sucesso!";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            Console.WriteLine(ex);
        }

        return response;
    }

    public async Task<ApiResponse<List<UserModel?>>> DeleteUserAsync(int id)
    {
        ApiResponse<List<UserModel?>> response = new();

        try
        {
            var user = await context.Users.FirstOrDefaultAsync(user => user.UserId == id);

            if (user == null)
            {
                response.Success = false;
                response.Message = "Nenhum usuário encontrado com o ID informado.";
                return response;
            }

            context.Remove(user);
            await context.SaveChangesAsync();

            response.Data = await context.Users.ToListAsync();
            response.Success = true;
            response.Message = $"Usuário {id} deletado com sucesso!";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            Console.WriteLine(ex);
        }

        return response;
    }

    public Task<List<UserModel>> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<UserModel?> GetUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<UserModel?> GetUserAsync(Expression<Func<UserModel?, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<UserModel?> CreateUserAsync(UserModel? user)
    {
        throw new NotImplementedException();
    }

    public Task<UserModel?> UpdateUserAsync(UserModel? user)
    {
        throw new NotImplementedException();
    }

    public Task<UserModel?> DeleteUserAsync(UserModel? user)
    {
        throw new NotImplementedException();
    }
}
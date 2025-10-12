using Auth.Api.Dto;
using Auth.Api.Models;
using Auth.Api.Models.Response;

namespace Auth.Api.Repositories.UserRepository;

public interface IUserRepository
{
    Task<ApiResponse<UserModel>> GetByIdAsync(int id);
    Task<ApiResponse<List<UserModel>>> GetAllAsync();
    Task<ApiResponse<List<UserModel>>> CreateUserAsync(CreateUserDto createUserDto);
    Task<ApiResponse<List<UserModel>>> UpdateUserAsync(UpdateUserDto updateUserDto);
    Task<ApiResponse<List<UserModel>>> DeleteUserAsync(int id);
}
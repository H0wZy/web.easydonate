using User.Api.Dto;
using User.Api.Models;

namespace User.Api.Services.UserService;

public interface IUserService
{
    public Task<ResponseModel<List<UserDto>?>> GetAllUsersAsync();
    public Task<ResponseModel<UserDto?>> GetUserByIdAsync(int id);
    public Task<ResponseModel<UserDto?>> GetUserByUsernameAsync(string username);
    public Task<ResponseModel<UserDto?>> GetUserByEmailAsync(string email);
    public Task<ResponseModel<UserDto?>> CreateUserAsync(CreateUserDto dto);
    public Task<ResponseModel<UserDto?>> UpdateUserByIdAsync(int id, UpdateUserDto dto);
    public Task<ResponseModel<object?>> DeleteUserByIdAsync(int id);
    public Task<ResponseModel<UserDto?>> DisableUserByIdAsync(int id);
}
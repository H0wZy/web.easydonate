using System.Linq.Expressions;
using User.Api.Dto;
using User.Api.Models;

namespace User.Api.Services.UserService;

public interface IUserService
{
    public Task<ResponseModel<List<UserModel>>> GetAllUsersAsync();
    public Task<ResponseModel<UserModel>> GetUserByIdAsync(int id);
    public Task<ResponseModel<UserModel>> GetUserByUsernameAsync(string username);
    public Task<ResponseModel<UserModel>> GetUserByEmailAsync(string email);
    public Task<ResponseModel<UserModel>> CreateUserAsync(CreateUserDto dto);
    public Task<ResponseModel<UserModel>> UpdateUserByIdAsync(int id, UpdateUserDto dto);
    public Task<ResponseModel<UserModel>> DeleteUserByIdAsync(int id);
    public Task<ResponseModel<UserModel>> DisableUserByIdAsync(int id);
}
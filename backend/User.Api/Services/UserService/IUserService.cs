using System.Linq.Expressions;
using User.Api.Dto;
using User.Api.Models;

namespace User.Api.Services.UserService;

public interface IUserService
{
    public Task<ResponseModel<List<UserModel>>> GetAllUsersAsync();
    public Task<ResponseModel<UserModel>> GetUserByIdAsync(int id);
    public Task<ResponseModel<UserModel>> CreateUserAsync(CreateUserDto dto);
    public Task<ResponseModel<UserModel>> UpdateUserAsync(UpdateUserDto dto);
    public Task<ResponseModel<UserModel>> DeleteUserAsync(int id);
}
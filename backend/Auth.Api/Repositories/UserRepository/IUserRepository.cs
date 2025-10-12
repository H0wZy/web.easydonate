using System.Linq.Expressions;
using Auth.Api.Dto;
using Auth.Api.Models;
using Auth.Api.Models.Response;

namespace Auth.Api.Repositories.UserRepository;

public interface IUserRepository
{
    Task<List<UserModel>> GetAllUsersAsync();
    Task<UserModel?> GetUserByIdAsync(int id);
    Task<UserModel?> GetUserAsync(Expression<Func<UserModel?, bool>> predicate);
    Task<UserModel?> CreateUserAsync(UserModel? user);
    Task<UserModel?> UpdateUserAsync(UserModel? user);
    Task<UserModel?> DeleteUserAsync(UserModel? user);
}
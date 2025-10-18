using System.Linq.Expressions;
using User.Api.Dto;
using User.Api.Models;

namespace User.Api.Repositories.UserRepository;

public interface IUserRepository
{
    Task<List<UserModel>> GetAllUsersAsync();
    Task<UserModel?> GetUserByIdAsync(int id);
    Task<UserModel?> GetUserAsync(Expression<Func<UserModel, bool>> predicate);
    Task<UserModel> CreateUserAsync(UserModel user);
    Task<UserModel> UpdateUserAsync(UserModel user);
    Task<UserModel> DeleteUserAsync(UserModel user);
}
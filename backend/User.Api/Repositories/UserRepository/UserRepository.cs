using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using User.Api.Data;
using User.Api.Models;

namespace User.Api.Repositories.UserRepository;

public class UserRepository(UserDbContext context) : IUserRepository
{
    public async Task<List<UserModel>> GetAllUsersAsync()
    {
        return await context.Users.ToListAsync();
    }

    public async Task<UserModel?> GetUserByIdAsync(int id)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.UserId == id);
    }

    public async Task<UserModel?> GetUserAsync(Expression<Func<UserModel?, bool>> predicate)
    {
        return await context.Users.FirstOrDefaultAsync(predicate);
    }

    public async Task<UserModel> CreateUserAsync(UserModel user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<UserModel> UpdateUserAsync(UserModel user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<UserModel> DeleteUserAsync(UserModel user)
    {
        context.Users.Remove(user);
        await context.SaveChangesAsync();
        return user;
    }
}
using User.Api.Models;

namespace User.Api.Services.UserService;

public interface IUserService
{
    public Task<RespondeModel<List<UserModel>>> GetAllUsersAsync();
}
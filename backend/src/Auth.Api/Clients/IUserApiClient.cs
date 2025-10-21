using Auth.Api.Dto;

namespace Auth.Api.Clients;

public interface IUserApiClient
{
    Task<UserDto?> GetUserByEmailAsync(string email);
    Task<UserDto?> GetUserByUsernameAsync(string username);
    Task<UserDto?> GetUserByIdAsync(int id);
}
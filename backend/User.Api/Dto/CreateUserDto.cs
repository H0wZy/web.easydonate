using User.Api.Enum;

namespace User.Api.Dto;

public record CreateUserDto(
    string Username,
    string Email,
    string FirstName,
    string LastName,
    string Password,
    UserType UserType);
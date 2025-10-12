using Auth.Api.Enum;

namespace Auth.Api.Dto;

public record CreateUserDto(
    string Username,
    string Email,
    string FirstName,
    string LastName,
    string Password,
    UserType UserType);
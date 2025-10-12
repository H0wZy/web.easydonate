using Auth.Api.Enum;

namespace Auth.Api.Dto;

public record UpdateUserDto(
    int UserId,
    string Username,
    string Email,
    string FirstName,
    string LastName);
using User.Api.Enum;

namespace User.Api.Dto;

public record UpdateUserDto(
    int UserId,
    string Username,
    string Email,
    string FirstName,
    string LastName);
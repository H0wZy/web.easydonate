using User.Api.Enum;

namespace User.Api.Dto;

public record UpdateUserDto(
    int Id,
    string Username,
    string Email,
    string Firstname,
    string Lastname);
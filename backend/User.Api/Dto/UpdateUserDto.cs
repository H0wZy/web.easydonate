namespace User.Api.Dto;

public record UpdateUserDto(
    string? Username,
    string? Email,
    string? Firstname,
    string? Lastname);
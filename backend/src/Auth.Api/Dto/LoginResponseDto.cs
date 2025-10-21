namespace Auth.Api.Dto;

public record LoginResponseDto(
    string Token,
    string Username,
    string Email,
    string Firstname,
    string Lastname);
namespace Auth.Api.Dto;

public record LoginResponseDto(
    string Token,
    DateTime ExpiresIn);
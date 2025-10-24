using System;

namespace Auth.Api.Dto
{
    public record TokenResponseDto
    {
        public string TokenType { get; init; } = "Bearer";
        public required string AccessToken { get; init; }
        public required DateTime ExpiresAt { get; init; }
        public long MinutesToExpire { get; init; }
        public string? LoginMethod { get; init; }
        public DateTime? LastLoginAt { get; init; }
    }
}
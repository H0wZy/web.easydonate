using System;

namespace Auth.Api.Dto
{
    public record TokenResponseDto
    {
        public string TokenType { get; init; } = "Bearer";
        public string AccessToken { get; init; }
        public DateTime ExpiresAt { get; init; }
    }
}
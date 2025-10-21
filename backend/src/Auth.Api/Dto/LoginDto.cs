using System.ComponentModel.DataAnnotations;

namespace Auth.Api.Dto;

public record LoginDto(
    [Required] string Username,
    [Required] string Email,
    [Required] string Password
);
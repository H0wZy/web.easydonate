using System.ComponentModel.DataAnnotations;

namespace Auth.Api.Dto;

public record LoginDto(
    [Required] string Login,
    [Required] string Password
);
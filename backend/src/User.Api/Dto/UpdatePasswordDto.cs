namespace User.Api.Dto;

public record UpdatePasswordDto
{
    public required string CurrentPassword { get; init; }
    public required string NewPassword { get; init; }
}

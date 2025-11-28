namespace User.Api.Dto;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string HashPassword { get; set; } = string.Empty;
    public string SaltPassword { get; set; } = string.Empty;
    public int UserType { get; set; }
    public bool IsUserDisabled { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
}

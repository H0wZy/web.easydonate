namespace Auth.Api.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string HashPassword { get; set; }
        public string SaltPassword { get; set; }
        public int UserType { get; set; }
        public bool IsUserDisabled { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
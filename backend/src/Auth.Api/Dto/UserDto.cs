using System;

namespace Auth.Api.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string HashPassword { get; set; }
        public string SaltPassword { get; set; }
        public int UserType { get; set; }
        public bool IsUserDisabled { get; set; }
        public DateTime? LastLoginAt { get; set; }
    }
}
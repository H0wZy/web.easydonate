using System.Security.Cryptography;
using System.Linq;

namespace Auth.Api.Utils;

public static class PasswordHelper
{
    public static (byte[] hash, byte[] salt) HashPassword(string password)
    {
        var salt = new byte[16];
        RandomNumberGenerator.Fill(salt);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 10000, HashAlgorithmName.SHA256, 32);
        return (hash, salt);
    }

    public static bool VerifyPassword(string password, byte[] savedHash, byte[] savedSalt)
    {
        var verifyHash = Rfc2898DeriveBytes.Pbkdf2(password, savedSalt, 10000, HashAlgorithmName.SHA256, 32);
        return verifyHash.SequenceEqual(savedHash);
    }
}
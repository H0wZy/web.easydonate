using System.Security.Cryptography;
using System.Linq;

namespace Auth.Api.Utils;

public static class PasswordHelper
{
    public static bool VerifyPassword(string password, byte[] savedHash, byte[] savedSalt)
    {
        var verifyHash = Rfc2898DeriveBytes.Pbkdf2(password, savedSalt, 10000, HashAlgorithmName.SHA256, 32);
        return verifyHash.SequenceEqual(savedHash);
    }
}
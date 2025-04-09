using System.Security.Cryptography;

namespace ChanceGame.Security;

public static class SecurityUtils
{
    public static string HashPassword(string password, int iterations = 100_000)
    {
        
        byte[] salt = RandomNumberGenerator.GetBytes(16); 
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32); 
        var hashBytes = new byte[1 + 4 + salt.Length + hash.Length];
        hashBytes[0] = 1; 
        BitConverter.GetBytes(iterations).CopyTo(hashBytes, 1);
        salt.CopyTo(hashBytes, 5);
        hash.CopyTo(hashBytes, 5 + salt.Length);
        return Convert.ToBase64String(hashBytes);
    }
    
    public static bool VerifyPassword(string password, string storedHash)
    {
        try
        {
            byte[] hashBytes = Convert.FromBase64String(storedHash);
            if (hashBytes[0] != 1)
                return false; 
            int iterations = BitConverter.ToInt32(hashBytes, 1);
            byte[] salt = new byte[16];
            Buffer.BlockCopy(hashBytes, 5, salt, 0, 16);
            byte[] storedSubkey = new byte[32];
            Buffer.BlockCopy(hashBytes, 21, storedSubkey, 0, 32);
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            byte[] generatedSubkey = pbkdf2.GetBytes(32);
            return CryptographicOperations.FixedTimeEquals(storedSubkey, generatedSubkey);
        }
        catch
        {
            return false;
        }
    }
}
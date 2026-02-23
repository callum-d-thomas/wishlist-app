using System.Security.Cryptography;
using System.Text;
using WishlistApi.Core.Interfaces;

namespace WishlistApi.Data.Services;

/// <summary>
/// PBKDF2-based password hashing service using built-in .NET crypto.
/// Uses Rfc2898DeriveBytes for secure password hashing.
/// </summary>
public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 10000;

    public string HashPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Password cannot be empty", nameof(password));
        }

        // Generate a random salt
        using (var rng = RandomNumberGenerator.Create())
        {
            var salt = new byte[SaltSize];
            rng.GetBytes(salt);

            // Derive hash from password using PBKDF2 static method
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, HashSize);

            // Combine salt + hash and encode as base64
            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            return Convert.ToBase64String(hashBytes);
        }
    }

    public bool VerifyPassword(string password, string hash)
    {
        if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hash))
        {
            return false;
        }

        try
        {
            // Decode the base64 hash to extract salt and hash
            var hashBytes = Convert.FromBase64String(hash);

            if (hashBytes.Length != SaltSize + HashSize)
            {
                return false;
            }

            // Extract salt from the stored hash
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Derive hash from provided password using the stored salt
            var computedHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, HashSize);

            // Compare computed hash with stored hash
            return ConstantTimeEquals(computedHash, hashBytes, SaltSize);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Constant-time comparison to prevent timing attacks.
    /// </summary>
    private static bool ConstantTimeEquals(byte[] computed, byte[] stored, int saltSize)
    {
        int result = 0;

        for (int i = 0; i < computed.Length; i++)
        {
            result |= computed[i] ^ stored[saltSize + i];
        }

        return result == 0;
    }
}

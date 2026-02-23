namespace WishlistApi.Core.Interfaces;

/// <summary>
/// Interface for JWT token generation and validation.
/// </summary>
public interface IJwtTokenService
{
    /// <summary>
    /// Generate a JWT access token for a user.
    /// </summary>
    string GenerateAccessToken(Guid userId, string email);

    /// <summary>
    /// Generate a JWT refresh token for a user (optional, for future use).
    /// </summary>
    string GenerateRefreshToken();
}

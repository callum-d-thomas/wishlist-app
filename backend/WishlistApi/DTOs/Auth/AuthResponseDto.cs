namespace WishlistApi.DTOs.Auth;

/// <summary>
/// DTO for authentication response (successful login/registration).
/// </summary>
public record AuthResponseDto(
    Guid UserId,
    string Email,
    string FirstName,
    string LastName,
    string Token,
    DateTime ExpiresAt
);

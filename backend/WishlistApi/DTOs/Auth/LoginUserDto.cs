namespace WishlistApi.DTOs.Auth;

/// <summary>
/// DTO for user login request.
/// </summary>
public record LoginUserDto(
    string Email,
    string Password
);

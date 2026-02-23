namespace WishlistApi.DTOs.Auth;

/// <summary>
/// DTO for user registration request.
/// </summary>
public record RegisterUserDto(
    string Email,
    string Password,
    string FirstName,
    string LastName
);

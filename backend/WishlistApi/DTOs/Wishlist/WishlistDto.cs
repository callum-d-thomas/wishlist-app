namespace WishlistApi.DTOs.Wishlist;

/// <summary>
/// DTO for wishlist response.
/// </summary>
public record WishlistDto(
    Guid Id,
    Guid OwnerId,
    string OwnerName,
    string Title,
    string? Description,
    string? Occasion,
    DateTime? EventDate,
    int ItemCount,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

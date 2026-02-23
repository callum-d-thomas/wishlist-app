namespace WishlistApi.DTOs.Wishlist;

/// <summary>
/// DTO for updating an existing wishlist.
/// </summary>
public record UpdateWishlistDto(
    string Title,
    string? Description,
    string? Occasion,
    DateTime? EventDate
);

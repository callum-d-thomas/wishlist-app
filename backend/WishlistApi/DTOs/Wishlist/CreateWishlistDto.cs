namespace WishlistApi.DTOs.Wishlist;

/// <summary>
/// DTO for creating a new wishlist.
/// </summary>
public record CreateWishlistDto(
    string Title,
    string? Description,
    string? Occasion,
    DateTime? EventDate
);

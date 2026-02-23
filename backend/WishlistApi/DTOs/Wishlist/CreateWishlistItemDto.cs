namespace WishlistApi.DTOs.Wishlist;

/// <summary>
/// DTO for creating a wishlist item.
/// </summary>
public record CreateWishlistItemDto(
    string Name,
    string? Description,
    string? Url,
    string? ImageUrl,
    decimal? Price,
    int Priority = 0
);

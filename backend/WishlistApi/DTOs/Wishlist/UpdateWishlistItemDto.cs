namespace WishlistApi.DTOs.Wishlist;

/// <summary>
/// DTO for updating a wishlist item.
/// </summary>
public record UpdateWishlistItemDto(
    string Name,
    string? Description,
    string? Url,
    string? ImageUrl,
    decimal? Price,
    int Priority
);

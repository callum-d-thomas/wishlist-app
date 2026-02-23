namespace WishlistApi.DTOs.Wishlist;

/// <summary>
/// DTO for claiming or unclaiming an item.
/// </summary>
public record ClaimItemDto(
    Guid UserId
);

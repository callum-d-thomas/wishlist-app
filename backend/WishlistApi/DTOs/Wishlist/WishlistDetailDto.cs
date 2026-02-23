namespace WishlistApi.DTOs.Wishlist;

/// <summary>
/// DTO for wishlist detail with items.
/// </summary>
public record WishlistDetailDto(
    Guid Id,
    Guid OwnerId,
    string OwnerName,
    string Title,
    string? Description,
    string? Occasion,
    DateTime? EventDate,
    List<WishlistItemDto> Items,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record WishlistItemDto(
    Guid Id,
    string Name,
    string? Description,
    string? Url,
    string? ImageUrl,
    decimal? Price,
    int Priority,
    bool IsClaimed,
    Guid? ClaimedByUserId,
    string? ClaimedByName,
    DateTime? ClaimedAt
);

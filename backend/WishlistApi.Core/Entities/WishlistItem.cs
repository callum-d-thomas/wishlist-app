namespace WishlistApi.Core.Entities;

/// <summary>
/// Represents an item in a wishlist.
/// </summary>
public class WishlistItem
{
    /// <summary>
    /// Gets the unique identifier for the item.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets the ID of the wishlist this item belongs to.
    /// </summary>
    public Guid WishlistId { get; private set; }

    /// <summary>
    /// Gets the wishlist this item belongs to.
    /// </summary>
    public Wishlist Wishlist { get; private set; } = null!;

    /// <summary>
    /// Gets the name of the item.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the description of the item.
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    /// Gets the URL where the item can be purchased.
    /// </summary>
    public string? Url { get; private set; }

    /// <summary>
    /// Gets the URL of an image for the item.
    /// </summary>
    public string? ImageUrl { get; private set; }

    /// <summary>
    /// Gets the approximate price of the item.
    /// </summary>
    public decimal? Price { get; private set; }

    /// <summary>
    /// Gets the priority level (higher number = higher priority).
    /// </summary>
    public int Priority { get; private set; }

    /// <summary>
    /// Gets whether the item has been claimed.
    /// </summary>
    public bool IsClaimed { get; private set; }

    /// <summary>
    /// Gets the ID of the user who claimed the item.
    /// </summary>
    public Guid? ClaimedByUserId { get; private set; }

    /// <summary>
    /// Gets the user who claimed the item.
    /// </summary>
    public User? ClaimedBy { get; private set; }

    /// <summary>
    /// Gets the date and time when the item was claimed.
    /// </summary>
    public DateTime? ClaimedAt { get; private set; }

    /// <summary>
    /// Gets the date and time when the item was created.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Gets the date and time when the item was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor
    private WishlistItem()
    {
        // Required by EF Core
    }
#pragma warning restore CS8618

    public WishlistItem(Guid wishlistId, string name, string? description = null, string? url = null, string? imageUrl = null, decimal? price = null, int priority = 0)
    {
        Id = Guid.NewGuid();
        WishlistId = wishlistId;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
        Url = url;
        ImageUrl = imageUrl;
        Price = price;
        Priority = priority;
        IsClaimed = false;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateDetails(string name, string? description, string? url, string? imageUrl, decimal? price, int priority)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
        Url = url;
        ImageUrl = imageUrl;
        Price = price;
        Priority = priority;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Claim(Guid userId)
    {
        if (IsClaimed)
            throw new InvalidOperationException("Item is already claimed.");

        IsClaimed = true;
        ClaimedByUserId = userId;
        ClaimedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Unclaim()
    {
        if (!IsClaimed)
            throw new InvalidOperationException("Item is not claimed.");

        IsClaimed = false;
        ClaimedByUserId = null;
        ClaimedAt = null;
        UpdatedAt = DateTime.UtcNow;
    }
}

namespace WishlistApi.Core.Entities;

/// <summary>
/// Represents a wishlist aggregate root.
/// A wishlist contains items that a user wants and can be shared with others.
/// </summary>
public class Wishlist
{
    /// <summary>
    /// Gets the unique identifier for the wishlist.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets the ID of the user who owns this wishlist.
    /// </summary>
    public Guid OwnerId { get; private set; }

    /// <summary>
    /// Gets the owner of this wishlist.
    /// </summary>
    public User Owner { get; private set; } = null!;

    /// <summary>
    /// Gets the title of the wishlist.
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    /// Gets the description of the wishlist.
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    /// Gets the occasion for this wishlist (e.g., "Birthday", "Christmas").
    /// </summary>
    public string? Occasion { get; private set; }

    /// <summary>
    /// Gets the event date for this wishlist (e.g., birthday date).
    /// </summary>
    public DateTime? EventDate { get; private set; }

    /// <summary>
    /// Gets the date and time when the wishlist was created.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Gets the date and time when the wishlist was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; private set; }

    /// <summary>
    /// Gets the items in this wishlist.
    /// </summary>
    public ICollection<WishlistItem> Items { get; private set; } = new List<WishlistItem>();

    /// <summary>
    /// Gets the members who have access to this wishlist.
    /// </summary>
    public ICollection<WishlistMember> Members { get; private set; } = new List<WishlistMember>();

    /// <summary>
    /// Gets the pending invitations for this wishlist.
    /// </summary>
    public ICollection<WishlistInvitation> Invitations { get; private set; } = new List<WishlistInvitation>();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor
    private Wishlist()
    {
        // Required by EF Core
    }
#pragma warning restore CS8618

    public Wishlist(Guid ownerId, string title, string? description = null, string? occasion = null, DateTime? eventDate = null)
    {
        Id = Guid.NewGuid();
        OwnerId = ownerId;
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Description = description;
        Occasion = occasion;
        EventDate = eventDate;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        Items = new List<WishlistItem>();
        Members = new List<WishlistMember>();
        Invitations = new List<WishlistInvitation>();
    }

    public void UpdateDetails(string title, string? description, string? occasion, DateTime? eventDate)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Description = description;
        Occasion = occasion;
        EventDate = eventDate;
        UpdatedAt = DateTime.UtcNow;
    }

    public WishlistItem AddItem(string name, string? description = null, string? url = null, string? imageUrl = null, decimal? price = null, int priority = 0)
    {
        var item = new WishlistItem(Id, name, description, url, imageUrl, price, priority);
        Items.Add(item);
        UpdatedAt = DateTime.UtcNow;
        return item;
    }

    public void RemoveItem(Guid itemId)
    {
        var item = Items.FirstOrDefault(i => i.Id == itemId);
        if (item is not null)
        {
            Items.Remove(item);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public bool IsOwner(Guid userId) => OwnerId == userId;

    public bool IsMember(Guid userId) => IsOwner(userId) || Members.Any(m => m.UserId == userId);
}

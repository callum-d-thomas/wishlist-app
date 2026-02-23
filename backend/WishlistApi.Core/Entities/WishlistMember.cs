namespace WishlistApi.Core.Entities;

/// <summary>
/// Represents a user's membership in a wishlist.
/// This is a join entity between User and Wishlist.
/// </summary>
public class WishlistMember
{
    /// <summary>
    /// Gets the ID of the wishlist.
    /// </summary>
    public Guid WishlistId { get; private set; }

    /// <summary>
    /// Gets the wishlist.
    /// </summary>
    public Wishlist Wishlist { get; private set; } = null!;

    /// <summary>
    /// Gets the ID of the user.
    /// </summary>
    public Guid UserId { get; private set; }

    /// <summary>
    /// Gets the user.
    /// </summary>
    public User User { get; private set; } = null!;

    /// <summary>
    /// Gets the role of the member (e.g., "Viewer", "Contributor").
    /// </summary>
    public string Role { get; private set; }

    /// <summary>
    /// Gets the date and time when the invitation was sent.
    /// </summary>
    public DateTime InvitedAt { get; private set; }

    /// <summary>
    /// Gets the date and time when the user joined the wishlist.
    /// </summary>
    public DateTime? JoinedAt { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor
    private WishlistMember()
    {
        // Required by EF Core
    }
#pragma warning restore CS8618

    public WishlistMember(Guid wishlistId, Guid userId, string role = "Viewer")
    {
        WishlistId = wishlistId;
        UserId = userId;
        Role = role ?? throw new ArgumentNullException(nameof(role));
        InvitedAt = DateTime.UtcNow;
    }

    public void AcceptInvitation()
    {
        JoinedAt = DateTime.UtcNow;
    }

    public void UpdateRole(string role)
    {
        Role = role ?? throw new ArgumentNullException(nameof(role));
    }
}

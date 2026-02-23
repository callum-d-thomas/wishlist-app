namespace WishlistApi.Core.Entities;

/// <summary>
/// Represents an invitation to join a wishlist.
/// </summary>
public class WishlistInvitation
{
    /// <summary>
    /// Gets the unique identifier for the invitation.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets the ID of the wishlist.
    /// </summary>
    public Guid WishlistId { get; private set; }

    /// <summary>
    /// Gets the wishlist.
    /// </summary>
    public Wishlist Wishlist { get; private set; } = null!;

    /// <summary>
    /// Gets the email address of the invitee.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Gets the unique token for this invitation.
    /// </summary>
    public string Token { get; private set; }

    /// <summary>
    /// Gets the expiration date and time for this invitation.
    /// </summary>
    public DateTime ExpiresAt { get; private set; }

    /// <summary>
    /// Gets whether the invitation has been accepted.
    /// </summary>
    public bool IsAccepted { get; private set; }

    /// <summary>
    /// Gets the date and time when the invitation was created.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor
    private WishlistInvitation()
    {
        // Required by EF Core
    }
#pragma warning restore CS8618

    public WishlistInvitation(Guid wishlistId, string email, int expirationDays = 7)
    {
        Id = Guid.NewGuid();
        WishlistId = wishlistId;
        Email = email ?? throw new ArgumentNullException(nameof(email));
        Token = GenerateToken();
        ExpiresAt = DateTime.UtcNow.AddDays(expirationDays);
        IsAccepted = false;
        CreatedAt = DateTime.UtcNow;
    }

    public void Accept()
    {
        if (IsAccepted)
            throw new InvalidOperationException("Invitation has already been accepted.");

        if (DateTime.UtcNow > ExpiresAt)
            throw new InvalidOperationException("Invitation has expired.");

        IsAccepted = true;
    }

    public bool IsExpired() => DateTime.UtcNow > ExpiresAt;

    private static string GenerateToken()
    {
        return Convert.ToBase64String(Guid.NewGuid().ToByteArray())
            .Replace("+", "")
            .Replace("/", "")
            .Replace("=", "")
            .Substring(0, 22);
    }
}

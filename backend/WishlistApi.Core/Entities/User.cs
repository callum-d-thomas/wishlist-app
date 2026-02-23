namespace WishlistApi.Core.Entities;

/// <summary>
/// Represents a user account in the system.
/// </summary>
public class User
{
    /// <summary>
    /// Gets the unique identifier for the user.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets the user's email address (used for login).
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Gets the hashed password.
    /// </summary>
    public string PasswordHash { get; private set; }

    /// <summary>
    /// Gets the user's first name.
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    /// Gets the user's last name.
    /// </summary>
    public string LastName { get; private set; }

    /// <summary>
    /// Gets the date and time when the user account was created.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Gets the date and time when the user account was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; private set; }

    /// <summary>
    /// Gets the wishlists owned by this user.
    /// </summary>
    public ICollection<Wishlist> OwnedWishlists { get; private set; }

    /// <summary>
    /// Gets the wishlist memberships for this user.
    /// </summary>
    public ICollection<WishlistMember> WishlistMemberships { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor
    private User()
    {
        // Required by EF Core
    }
#pragma warning restore CS8618

    public User(string email, string passwordHash, string firstName, string lastName)
    {
        Id = Guid.NewGuid();
        Email = email ?? throw new ArgumentNullException(nameof(email));
        PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        OwnedWishlists = new List<Wishlist>();
        WishlistMemberships = new List<WishlistMember>();
    }

    public void UpdateProfile(string firstName, string lastName)
    {
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdatePassword(string passwordHash)
    {
        PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Factory method to create a new user (follows DDD pattern).
    /// </summary>
    public static User Create(string email, string passwordHash, string firstName, string lastName)
    {
        return new User(email, passwordHash, firstName, lastName);
    }
}

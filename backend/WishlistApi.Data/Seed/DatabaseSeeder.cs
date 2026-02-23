using WishlistApi.Core.Entities;
using WishlistApi.Core.Interfaces;
using WishlistApi.Data.Context;

namespace WishlistApi.Data.Seed;

/// <summary>
/// Utility class for seeding initial database data.
/// </summary>
public static class DatabaseSeeder
{
    /// <summary>
    /// Seed the database with initial test data.
    /// </summary>
    public static async Task SeedAsync(ApplicationDbContext context, IPasswordHasher passwordHasher)
    {
        // Only seed if database is empty
        if (context.Users.Any())
        {
            return;
        }

        // Create test users
        var testUsers = new List<User>
        {
            User.Create(
                email: "alice@example.com",
                passwordHash: passwordHasher.HashPassword("Password123"),
                firstName: "Alice",
                lastName: "Johnson"),
            User.Create(
                email: "bob@example.com",
                passwordHash: passwordHasher.HashPassword("Password123"),
                firstName: "Bob",
                lastName: "Smith"),
            User.Create(
                email: "charlie@example.com",
                passwordHash: passwordHasher.HashPassword("Password123"),
                firstName: "Charlie",
                lastName: "Brown"),
        };

        await context.Users.AddRangeAsync(testUsers);

        // Create test wishlists and items
        var aliceWishlist = new Wishlist(
            ownerId: testUsers[0].Id,
            title: "Alice's Birthday Wishlist",
            description: "Things I'd love for my birthday!",
            occasion: "Birthday",
            eventDate: new DateTime(2026, 6, 15));

        aliceWishlist.AddItem("Book - Clean Code", "A Handbook of Agile Software Craftsmanship", 
            "https://www.amazon.com/Clean-Code-Handbook-Software-Craftsmanship/dp/0132350882", 
            null, 39.99m, 1);
        aliceWishlist.AddItem("Wireless Headphones", "High-quality noise-cancelling headphones",
            "https://www.example.com/headphones", null, 199.99m, 1);
        aliceWishlist.AddItem("Coffee Machine", "Programmable coffee maker for mornings",
            null, null, 89.99m, 2);

        var bobWishlist = new Wishlist(
            ownerId: testUsers[1].Id,
            title: "Bob's Holiday Wishlist",
            description: "Gifts for the holiday season",
            occasion: "Holiday",
            eventDate: new DateTime(2026, 12, 25));

        bobWishlist.AddItem("Programming Book", "Design Patterns: Elements of Reusable Object-Oriented Software",
            null, null, 45.99m, 1);
        bobWishlist.AddItem("Mechanical Keyboard", "RGB mechanical keyboard for gaming",
            null, null, 149.99m, 2);

        // Add members to wishlists (Bob and Charlie as members of Alice's wishlist, Charlie as member of Bob's)
        aliceWishlist.Members.Add(new WishlistMember(aliceWishlist.Id, testUsers[1].Id, "Contributor"));
        aliceWishlist.Members.Add(new WishlistMember(aliceWishlist.Id, testUsers[2].Id, "Contributor"));

        bobWishlist.Members.Add(new WishlistMember(bobWishlist.Id, testUsers[2].Id, "Viewer"));

        await context.Wishlists.AddRangeAsync(aliceWishlist, bobWishlist);
        await context.SaveChangesAsync();
    }
}

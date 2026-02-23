using Microsoft.EntityFrameworkCore;
using WishlistApi.Core.Entities;

namespace WishlistApi.Data.Context;

/// <summary>
/// Database context for the Wishlist application.
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Wishlist> Wishlists => Set<Wishlist>();
    public DbSet<WishlistItem> WishlistItems => Set<WishlistItem>();
    public DbSet<WishlistMember> WishlistMembers => Set<WishlistMember>();
    public DbSet<WishlistInvitation> WishlistInvitations => Set<WishlistInvitation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(256);
            
            entity.HasIndex(e => e.Email)
                .IsUnique();
            
            entity.Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(256);
            
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.UpdatedAt)
                .IsRequired();

            entity.HasMany(e => e.OwnedWishlists)
                .WithOne(e => e.Owner)
                .HasForeignKey(e => e.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Wishlist configuration
        modelBuilder.Entity<Wishlist>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);
            
            entity.Property(e => e.Description)
                .HasMaxLength(1000);
            
            entity.Property(e => e.Occasion)
                .HasMaxLength(100);

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.UpdatedAt)
                .IsRequired();

            entity.HasMany(e => e.Items)
                .WithOne(e => e.Wishlist)
                .HasForeignKey(e => e.WishlistId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.Invitations)
                .WithOne(e => e.Wishlist)
                .HasForeignKey(e => e.WishlistId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // WishlistItem configuration
        modelBuilder.Entity<WishlistItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);
            
            entity.Property(e => e.Description)
                .HasMaxLength(1000);
            
            entity.Property(e => e.Url)
                .HasMaxLength(500);
            
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500);
            
            entity.Property(e => e.Price)
                .HasPrecision(18, 2);

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.UpdatedAt)
                .IsRequired();

            entity.HasOne(e => e.ClaimedBy)
                .WithMany()
                .HasForeignKey(e => e.ClaimedByUserId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasIndex(e => e.WishlistId);
        });

        // WishlistMember configuration (many-to-many join table)
        modelBuilder.Entity<WishlistMember>(entity =>
        {
            entity.HasKey(e => new { e.WishlistId, e.UserId });
            
            entity.Property(e => e.Role)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.InvitedAt)
                .IsRequired();

            entity.HasOne(e => e.Wishlist)
                .WithMany(e => e.Members)
                .HasForeignKey(e => e.WishlistId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.User)
                .WithMany(e => e.WishlistMemberships)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.UserId);
        });

        // WishlistInvitation configuration
        modelBuilder.Entity<WishlistInvitation>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(256);
            
            entity.Property(e => e.Token)
                .IsRequired()
                .HasMaxLength(256);
            
            entity.HasIndex(e => e.Token)
                .IsUnique();

            entity.Property(e => e.ExpiresAt)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.HasIndex(e => e.WishlistId);
        });
    }
}

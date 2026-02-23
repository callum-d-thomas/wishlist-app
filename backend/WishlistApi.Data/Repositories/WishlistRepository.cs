using Microsoft.EntityFrameworkCore;
using WishlistApi.Core.Entities;
using WishlistApi.Core.Interfaces;
using WishlistApi.Data.Context;

namespace WishlistApi.Data.Repositories;

/// <summary>
/// Repository implementation for Wishlist entities.
/// </summary>
public class WishlistRepository : IWishlistRepository
{
    private readonly ApplicationDbContext _context;

    public WishlistRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Wishlist?> GetByIdAsync(Guid id)
    {
        return await _context.Wishlists
            .Include(w => w.Owner)
            .FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<Wishlist?> GetByIdWithItemsAsync(Guid id)
    {
        return await _context.Wishlists
            .Include(w => w.Owner)
            .Include(w => w.Items)
                .ThenInclude(i => i.ClaimedBy)
            .Include(w => w.Members)
                .ThenInclude(m => m.User)
            .FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<IEnumerable<Wishlist>> GetByOwnerIdAsync(Guid ownerId)
    {
        return await _context.Wishlists
            .Include(w => w.Owner)
            .Include(w => w.Items)
            .Where(w => w.OwnerId == ownerId)
            .OrderByDescending(w => w.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Wishlist>> GetByMemberIdAsync(Guid userId)
    {
        return await _context.Wishlists
            .Include(w => w.Owner)
            .Include(w => w.Items)
            .Where(w => w.Members.Any(m => m.UserId == userId))
            .OrderByDescending(w => w.CreatedAt)
            .ToListAsync();
    }

    public async Task<Wishlist> AddAsync(Wishlist wishlist)
    {
        _context.Wishlists.Add(wishlist);
        await _context.SaveChangesAsync();
        return wishlist;
    }

    public async Task UpdateAsync(Wishlist wishlist)
    {
        _context.Wishlists.Update(wishlist);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var wishlist = await GetByIdAsync(id);
        if (wishlist is not null)
        {
            _context.Wishlists.Remove(wishlist);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Wishlists.AnyAsync(w => w.Id == id);
    }

    public async Task<bool> IsOwnerAsync(Guid wishlistId, Guid userId)
    {
        return await _context.Wishlists
            .AnyAsync(w => w.Id == wishlistId && w.OwnerId == userId);
    }

    public async Task<bool> IsMemberAsync(Guid wishlistId, Guid userId)
    {
        var wishlist = await _context.Wishlists
            .Include(w => w.Members)
            .FirstOrDefaultAsync(w => w.Id == wishlistId);

        return wishlist is not null && 
               (wishlist.OwnerId == userId || wishlist.Members.Any(m => m.UserId == userId));
    }
}

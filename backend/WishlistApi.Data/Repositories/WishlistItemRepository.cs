using Microsoft.EntityFrameworkCore;
using WishlistApi.Core.Entities;
using WishlistApi.Core.Interfaces;
using WishlistApi.Data.Context;

namespace WishlistApi.Data.Repositories;

/// <summary>
/// Repository implementation for WishlistItem entities.
/// </summary>
public class WishlistItemRepository : IWishlistItemRepository
{
    private readonly ApplicationDbContext _context;

    public WishlistItemRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<WishlistItem?> GetByIdAsync(Guid id)
    {
        return await _context.WishlistItems
            .Include(i => i.ClaimedBy)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<WishlistItem>> GetByWishlistIdAsync(Guid wishlistId)
    {
        return await _context.WishlistItems
            .Include(i => i.ClaimedBy)
            .Where(i => i.WishlistId == wishlistId)
            .OrderBy(i => i.Priority)
            .ThenBy(i => i.CreatedAt)
            .ToListAsync();
    }

    public async Task<WishlistItem> AddAsync(WishlistItem item)
    {
        _context.WishlistItems.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task UpdateAsync(WishlistItem item)
    {
        _context.WishlistItems.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var item = await GetByIdAsync(id);
        if (item is not null)
        {
            _context.WishlistItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.WishlistItems.AnyAsync(i => i.Id == id);
    }
}

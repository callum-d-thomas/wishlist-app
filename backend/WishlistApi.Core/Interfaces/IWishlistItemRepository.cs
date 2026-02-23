using WishlistApi.Core.Entities;

namespace WishlistApi.Core.Interfaces;

/// <summary>
/// Repository interface for WishlistItem entities.
/// </summary>
public interface IWishlistItemRepository
{
    Task<WishlistItem?> GetByIdAsync(Guid id);
    Task<IEnumerable<WishlistItem>> GetByWishlistIdAsync(Guid wishlistId);
    Task<WishlistItem> AddAsync(WishlistItem item);
    Task UpdateAsync(WishlistItem item);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}

using WishlistApi.Core.Entities;

namespace WishlistApi.Core.Interfaces;

/// <summary>
/// Repository interface for Wishlist entities.
/// </summary>
public interface IWishlistRepository
{
    Task<Wishlist?> GetByIdAsync(Guid id);
    Task<Wishlist?> GetByIdWithItemsAsync(Guid id);
    Task<IEnumerable<Wishlist>> GetByOwnerIdAsync(Guid ownerId);
    Task<IEnumerable<Wishlist>> GetByMemberIdAsync(Guid userId);
    Task<Wishlist> AddAsync(Wishlist wishlist);
    Task UpdateAsync(Wishlist wishlist);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<bool> IsOwnerAsync(Guid wishlistId, Guid userId);
    Task<bool> IsMemberAsync(Guid wishlistId, Guid userId);
}

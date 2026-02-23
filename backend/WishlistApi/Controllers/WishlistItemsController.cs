using Microsoft.AspNetCore.Mvc;
using WishlistApi.Core.Entities;
using WishlistApi.Core.Interfaces;
using WishlistApi.DTOs.Wishlist;

namespace WishlistApi.Controllers;

/// <summary>
/// API controller for managing wishlist items.
/// </summary>
[ApiController]
[Route("api/wishlists/{wishlistId}/items")]
public class WishlistItemsController : ControllerBase
{
    private readonly IWishlistRepository _wishlistRepository;
    private readonly IWishlistItemRepository _itemRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<WishlistItemsController> _logger;

    public WishlistItemsController(
        IWishlistRepository wishlistRepository,
        IWishlistItemRepository itemRepository,
        IUserRepository userRepository,
        ILogger<WishlistItemsController> logger)
    {
        _wishlistRepository = wishlistRepository;
        _itemRepository = itemRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    /// <summary>
    /// Get all items in a wishlist.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WishlistItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<WishlistItemDto>>> GetItems(Guid wishlistId)
    {
        var wishlist = await _wishlistRepository.GetByIdAsync(wishlistId);
        
        if (wishlist is null)
        {
            return NotFound(new { message = "Wishlist not found" });
        }

        var items = await _itemRepository.GetByWishlistIdAsync(wishlistId);
        var dtos = items.Select(MapToDto);
        
        return Ok(dtos);
    }

    /// <summary>
    /// Get a specific item.
    /// </summary>
    [HttpGet("{itemId}")]
    [ProducesResponseType(typeof(WishlistItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WishlistItemDto>> GetItem(Guid wishlistId, Guid itemId)
    {
        var wishlist = await _wishlistRepository.GetByIdAsync(wishlistId);
        
        if (wishlist is null)
        {
            return NotFound(new { message = "Wishlist not found" });
        }

        var item = await _itemRepository.GetByIdAsync(itemId);
        
        if (item is null || item.WishlistId != wishlistId)
        {
            return NotFound(new { message = "Item not found" });
        }

        return Ok(MapToDto(item));
    }

    /// <summary>
    /// Add an item to a wishlist.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(WishlistItemDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WishlistItemDto>> AddItem(Guid wishlistId, [FromBody] CreateWishlistItemDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            return BadRequest(new { message = "Item name is required" });
        }

        var wishlist = await _wishlistRepository.GetByIdAsync(wishlistId);
        
        if (wishlist is null)
        {
            return NotFound(new { message = "Wishlist not found" });
        }

        // Create the item (but don't add to collection yet to avoid EF tracking issues)
        var item = new WishlistItem(wishlistId, dto.Name, dto.Description, dto.Url, dto.ImageUrl, dto.Price, dto.Priority);
        
        // Save the item to the database
        await _itemRepository.AddAsync(item);

        _logger.LogInformation("Added item {ItemId} to wishlist {WishlistId}", item.Id, wishlistId);

        return CreatedAtAction(nameof(GetItem), new { wishlistId, itemId = item.Id }, MapToDto(item));
    }

    /// <summary>
    /// Update a wishlist item.
    /// </summary>
    [HttpPut("{itemId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateItem(Guid wishlistId, Guid itemId, [FromBody] UpdateWishlistItemDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            return BadRequest(new { message = "Item name is required" });
        }

        var item = await _itemRepository.GetByIdAsync(itemId);
        
        if (item is null || item.WishlistId != wishlistId)
        {
            return NotFound(new { message = "Item not found" });
        }

        item.UpdateDetails(dto.Name, dto.Description, dto.Url, dto.ImageUrl, dto.Price, dto.Priority);
        await _itemRepository.UpdateAsync(item);

        _logger.LogInformation("Updated item {ItemId}", itemId);

        return NoContent();
    }

    /// <summary>
    /// Delete a wishlist item.
    /// </summary>
    [HttpDelete("{itemId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteItem(Guid wishlistId, Guid itemId)
    {
        var item = await _itemRepository.GetByIdAsync(itemId);
        
        if (item is null || item.WishlistId != wishlistId)
        {
            return NotFound(new { message = "Item not found" });
        }

        await _itemRepository.DeleteAsync(itemId);

        _logger.LogInformation("Deleted item {ItemId}", itemId);

        return NoContent();
    }

    /// <summary>
    /// Claim an item (mark it as reserved by a user).
    /// </summary>
    [HttpPost("{itemId}/claim")]
    [ProducesResponseType(typeof(WishlistItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<WishlistItemDto>> ClaimItem(Guid wishlistId, Guid itemId, [FromBody] ClaimItemDto dto)
    {
        var item = await _itemRepository.GetByIdAsync(itemId);
        
        if (item is null || item.WishlistId != wishlistId)
        {
            return NotFound(new { message = "Item not found" });
        }

        var wishlist = await _wishlistRepository.GetByIdAsync(wishlistId);
        if (wishlist is null)
        {
            return NotFound(new { message = "Wishlist not found" });
        }

        // Verify user exists
        var user = await _userRepository.GetByIdAsync(dto.UserId);
        if (user is null)
        {
            return BadRequest(new { message = "User does not exist" });
        }

        // Verify user is not the owner
        if (wishlist.IsOwner(dto.UserId))
        {
            return BadRequest(new { message = "Wishlist owner cannot claim items" });
        }

        // Verify user is a member of the wishlist
        var isMember = await _wishlistRepository.IsMemberAsync(wishlistId, dto.UserId);
        if (!isMember)
        {
            return Forbid();
        }

        try
        {
            item.Claim(dto.UserId);
            await _itemRepository.UpdateAsync(item);

            _logger.LogInformation("User {UserId} claimed item {ItemId}", dto.UserId, itemId);

            var result = await _itemRepository.GetByIdAsync(itemId);
            return Ok(MapToDto(result!));
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Unclaim an item (remove the reservation).
    /// </summary>
    [HttpPost("{itemId}/unclaim")]
    [ProducesResponseType(typeof(WishlistItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<WishlistItemDto>> UnclaimItem(Guid wishlistId, Guid itemId, [FromBody] ClaimItemDto dto)
    {
        var item = await _itemRepository.GetByIdAsync(itemId);
        
        if (item is null || item.WishlistId != wishlistId)
        {
            return NotFound(new { message = "Item not found" });
        }

        try
        {
            item.Unclaim();
            await _itemRepository.UpdateAsync(item);

            _logger.LogInformation("User {UserId} unclaimed item {ItemId}", dto.UserId, itemId);

            var result = await _itemRepository.GetByIdAsync(itemId);
            return Ok(MapToDto(result!));
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    private static WishlistItemDto MapToDto(WishlistItemDto? existing, WishlistItem? item) =>
        item switch
        {
            not null => new WishlistItemDto(
                item.Id,
                item.Name,
                item.Description,
                item.Url,
                item.ImageUrl,
                item.Price,
                item.Priority,
                item.IsClaimed,
                item.ClaimedByUserId,
                item.ClaimedBy is not null ? $"{item.ClaimedBy.FirstName} {item.ClaimedBy.LastName}" : null,
                item.ClaimedAt
            ),
            _ => existing!
        };

    private static WishlistItemDto MapToDto(WishlistItem item) =>
        MapToDto(null, item);
}

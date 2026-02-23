using Microsoft.AspNetCore.Mvc;
using WishlistApi.Core.Entities;
using WishlistApi.Core.Interfaces;
using WishlistApi.DTOs.Wishlist;

namespace WishlistApi.Controllers;

/// <summary>
/// API controller for managing wishlists.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class WishlistsController : ControllerBase
{
    private readonly IWishlistRepository _wishlistRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<WishlistsController> _logger;

    public WishlistsController(
        IWishlistRepository wishlistRepository,
        IUserRepository userRepository,
        ILogger<WishlistsController> logger)
    {
        _wishlistRepository = wishlistRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    /// <summary>
    /// Get all wishlists owned by a specific user.
    /// </summary>
    [HttpGet("owner/{ownerId}")]
    [ProducesResponseType(typeof(IEnumerable<WishlistDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<WishlistDto>>> GetByOwnerId(Guid ownerId)
    {
        var wishlists = await _wishlistRepository.GetByOwnerIdAsync(ownerId);
        var dtos = wishlists.Select(MapToDto);
        return Ok(dtos);
    }

    /// <summary>
    /// Get a specific wishlist by ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(WishlistDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WishlistDetailDto>> GetById(Guid id)
    {
        var wishlist = await _wishlistRepository.GetByIdWithItemsAsync(id);
        
        if (wishlist is null)
        {
            return NotFound(new { message = "Wishlist not found" });
        }

        return Ok(MapToDetailDto(wishlist));
    }

    /// <summary>
    /// Create a new wishlist.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(WishlistDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<WishlistDto>> Create([FromBody] CreateWishlistDto dto, [FromQuery] Guid ownerId)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
        {
            return BadRequest(new { message = "Title is required" });
        }

        // Verify owner exists
        var ownerExists = await _userRepository.ExistsAsync(ownerId);
        if (!ownerExists)
        {
            return BadRequest(new { message = "Owner user does not exist" });
        }

        var wishlist = new Wishlist(
            ownerId,
            dto.Title,
            dto.Description,
            dto.Occasion,
            dto.EventDate
        );

        await _wishlistRepository.AddAsync(wishlist);
        
        _logger.LogInformation("Created wishlist {WishlistId} for user {UserId}", wishlist.Id, ownerId);

        var result = await _wishlistRepository.GetByIdAsync(wishlist.Id);
        return CreatedAtAction(nameof(GetById), new { id = wishlist.Id }, MapToDto(result!));
    }

    /// <summary>
    /// Update an existing wishlist.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWishlistDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
        {
            return BadRequest(new { message = "Title is required" });
        }

        var wishlist = await _wishlistRepository.GetByIdAsync(id);
        
        if (wishlist is null)
        {
            return NotFound(new { message = "Wishlist not found" });
        }

        wishlist.UpdateDetails(dto.Title, dto.Description, dto.Occasion, dto.EventDate);
        await _wishlistRepository.UpdateAsync(wishlist);

        _logger.LogInformation("Updated wishlist {WishlistId}", id);

        return NoContent();
    }

    /// <summary>
    /// Delete a wishlist.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var exists = await _wishlistRepository.ExistsAsync(id);
        
        if (!exists)
        {
            return NotFound(new { message = "Wishlist not found" });
        }

        await _wishlistRepository.DeleteAsync(id);

        _logger.LogInformation("Deleted wishlist {WishlistId}", id);

        return NoContent();
    }

    private static WishlistDto MapToDto(Wishlist wishlist)
    {
        return new WishlistDto(
            wishlist.Id,
            wishlist.OwnerId,
            $"{wishlist.Owner.FirstName} {wishlist.Owner.LastName}",
            wishlist.Title,
            wishlist.Description,
            wishlist.Occasion,
            wishlist.EventDate,
            wishlist.Items.Count,
            wishlist.CreatedAt,
            wishlist.UpdatedAt
        );
    }

    private static WishlistDetailDto MapToDetailDto(Wishlist wishlist)
    {
        return new WishlistDetailDto(
            wishlist.Id,
            wishlist.OwnerId,
            $"{wishlist.Owner.FirstName} {wishlist.Owner.LastName}",
            wishlist.Title,
            wishlist.Description,
            wishlist.Occasion,
            wishlist.EventDate,
            wishlist.Items.Select(item => new WishlistItemDto(
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
            )).ToList(),
            wishlist.CreatedAt,
            wishlist.UpdatedAt
        );
    }
}

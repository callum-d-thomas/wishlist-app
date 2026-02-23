using Microsoft.AspNetCore.Mvc;
using WishlistApi.Core.Entities;
using WishlistApi.Core.Interfaces;
using WishlistApi.DTOs.Auth;

namespace WishlistApi.Controllers;

/// <summary>
/// API controller for user authentication (registration and login).
/// </summary>
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _tokenService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenService tokenService,
        ILogger<AuthController> logger)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _logger = logger;
    }

    /// <summary>
    /// Register a new user account.
    /// </summary>
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterUserDto dto)
    {
        // Validation
        if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
        {
            return BadRequest(new { message = "Email and password are required" });
        }

        if (dto.Password.Length < 6)
        {
            return BadRequest(new { message = "Password must be at least 6 characters long" });
        }

        if (string.IsNullOrWhiteSpace(dto.FirstName) || string.IsNullOrWhiteSpace(dto.LastName))
        {
            return BadRequest(new { message = "First name and last name are required" });
        }

        // Check if user already exists
        var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
        if (existingUser is not null)
        {
            return Conflict(new { message = "Email already registered" });
        }

        try
        {
            // Create new user (factory method pattern from domain)
            var passwordHash = _passwordHasher.HashPassword(dto.Password);
            var user = WishlistApi.Core.Entities.User.Create(dto.Email, passwordHash, dto.FirstName, dto.LastName);

            await _userRepository.AddAsync(user);

            _logger.LogInformation("New user registered: {Email}", dto.Email);

            // Generate JWT token
            var token = _tokenService.GenerateAccessToken(user.Id, user.Email);
            var expiresAt = DateTime.UtcNow.AddHours(1); // Match token expiration

            var response = new AuthResponseDto(
                UserId: user.Id,
                Email: user.Email,
                FirstName: user.FirstName,
                LastName: user.LastName,
                Token: token,
                ExpiresAt: expiresAt);

            return CreatedAtAction(nameof(Register), response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error registering user: {Email}", dto.Email);
            return StatusCode(StatusCodes.Status500InternalServerError, 
                new { message = "An error occurred during registration" });
        }
    }

    /// <summary>
    /// Authenticate user and generate JWT token.
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginUserDto dto)
    {
        // Validation
        if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
        {
            return BadRequest(new { message = "Email and password are required" });
        }

        // Find user by email
        var user = await _userRepository.GetByEmailAsync(dto.Email);
        if (user is null)
        {
            _logger.LogWarning("Login attempt for non-existent user: {Email}", dto.Email);
            return Unauthorized(new { message = "Invalid email or password" });
        }

        // Verify password
        if (!_passwordHasher.VerifyPassword(dto.Password, user.PasswordHash))
        {
            _logger.LogWarning("Failed login attempt for user: {Email}", dto.Email);
            return Unauthorized(new { message = "Invalid email or password" });
        }

        _logger.LogInformation("User logged in: {Email}", dto.Email);

        // Generate JWT token
        var token = _tokenService.GenerateAccessToken(user.Id, user.Email);
        var expiresAt = DateTime.UtcNow.AddHours(1); // Match token expiration

        var response = new AuthResponseDto(
            UserId: user.Id,
            Email: user.Email,
            FirstName: user.FirstName,
            LastName: user.LastName,
            Token: token,
            ExpiresAt: expiresAt);

        return Ok(response);
    }

    /// <summary>
    /// Get current authenticated user profile.
    /// </summary>
    [HttpGet("profile")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResponseDto>> GetProfile()
    {
        // Extract userId from JWT claims (will be set by middleware later)
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized(new { message = "Invalid or missing token" });
        }

        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
        {
            return Unauthorized(new { message = "User not found" });
        }

        var token = _tokenService.GenerateAccessToken(user.Id, user.Email);
        var expiresAt = DateTime.UtcNow.AddHours(1);

        var response = new AuthResponseDto(
            UserId: user.Id,
            Email: user.Email,
            FirstName: user.FirstName,
            LastName: user.LastName,
            Token: token,
            ExpiresAt: expiresAt);

        return Ok(response);
    }
}

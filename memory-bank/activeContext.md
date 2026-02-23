# Active Context

**Last Updated**: February 20, 2026

## Current Focus
**Phase 1 Complete!** Foundation is ready with EF Core, domain entities, repositories, and first API controller.

## Recent Changes
- ✅ Created `.github/copilot-instructions.md` with project overview
- ✅ Initialized backend .NET solution with 4 projects
- ✅ Set up project references following DDD layered architecture
- ✅ Initialized Angular 21+ frontend with standalone components
- ✅ Installed and configured Bootstrap 5.3.8+
- ✅ Created comprehensive README documentation for all areas
- ✅ Set up .gitignore for both backend and frontend
- ✅ Created database folder for SQL scripts
- ✅ Initialized memory bank structure
- ✅ Added Scalar.AspNetCore for API documentation
- ✅ Configured Scalar API Reference UI (accessible at /scalar)
- ✅ Updated all documentation to include --launch-profile https
- ✅ Removed WeatherForecast boilerplate code
- ✅ Installed Entity Framework Core packages
- ✅ Created domain entities with DDD principles
- ✅ Created ApplicationDbContext with EF Core configuration
- ✅ Created initial database migration (InitialCreate)
- ✅ Applied migration - database created successfully
- ✅ Created repository interfaces (IUserRepository, IWishlistRepository)
- ✅ Implemented repository classes with async methods
- ✅ Registered repositories in DI container
- ✅ Created DTOs for wishlist operations
- ✅ Implemented WishlistsController with full CRUD operations
- ✅ Added example HTTP requests to WishlistApi.http

## Next Steps (Priority Order)
1. Add wishlist items endpoints (POST/PUT/DELETE items)
2. Implement item claiming functionality
3. Add authentication (User registration/login with JWT)
4. Add authorization to controllers
5. Implement wishlist sharing and invitations
6. Create users controller for registration/profile management
7. Add validation with FluentValidation
8. Write unit tests for repositories and domain entities
9. Write integration tests for API endpoints

## Active Decisions

### Technology Decisions (Confirmed)
- ✅ Angular 21+ with standalone components and signals
- ✅ ASP.NET Core 10 with controller-based API
- ✅ SQL Server Express for database
- ✅ Bootstrap 5.3.8+ for styling
- ✅ xUnit for backend testing
- ✅ Jasmine/Karma for frontend testing (Angular default)
- ✅ Scalar for API documentation (instead of Swagger)

### Architectural Decisions (Confirmed)
- ✅ DDD layered architecture for backend
- ✅ Repository pattern for data access
- ✅ Feature-based organization for Angular
- ✅ JWT Bearer tokens for authentication (to be implemented)
- ✅ RESTful API design

### Pending Decisions
- How to handle image uploads (local storage vs cloud storage)?
- Email notification system (SMTP vs third-party service)?
- Session management strategy (cookies vs local storage)?
- E2E testing framework for Angular (Playwright vs Cypress)?

## Current Blockers
None at this time.

## Questions to Resolve
1. Should we implement real-time notifications, or is polling sufficient?
2. What's the maximum number of items per wishlist?
3. Should we support multiple image uploads per item?
4. Do we need soft deletes for wishlists/items?

## Recent Learnings
- Angular 21+ uses `input()` and `output()` functions instead of decorators
- .NET 10 has built-in container publishing support
- Memory bank structure helps maintain context across sessions

## Work In Progress
- Setting up initial project structure (nearly complete)

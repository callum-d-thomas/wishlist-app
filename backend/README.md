# Wishlist App - Backend

ASP.NET Core 10 Web API for the Wishlist App.

## 📁 Project Structure

```
backend/
├── WishlistApp.sln              # Solution file
├── WishlistApi/                 # Web API project
│   ├── Controllers/             # API controllers
│   ├── Program.cs               # Application entry point
│   └── appsettings.json         # Configuration
├── WishlistApi.Core/            # Domain layer
│   ├── Entities/                # Domain entities
│   ├── Interfaces/              # Repository & service interfaces
│   └── Services/                # Domain services
├── WishlistApi.Data/            # Infrastructure layer
│   ├── Context/                 # EF Core DbContext
│   ├── Repositories/            # Repository implementations
│   └── Migrations/              # EF Core migrations
└── WishlistApi.Tests/           # Unit & integration tests
```

## 🛠️ Built With

- **ASP.NET Core 10** - Web API framework
- **Entity Framework Core 10** - ORM for data access
- **SQL Server Express** - Database (for development)
- **xUnit** - Testing framework

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- SQL Server Express or LocalDB

### Installation

1. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

2. **Build the solution:**
   ```bash
   dotnet build
   ```

3. **Configure database connection:**
   
   Create `WishlistApi/appsettings.Development.json` (this file is gitignored):
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=WishlistAppDb;Trusted_Connection=true;MultipleActiveResultSets=true"
     },
     "Logging": {
       "LogLevel": {
         "Default": "Information",
         "Microsoft.AspNetCore": "Warning"
       }
     }
   }
   ```

4. **Run migrations** (once implemented):
   ```bash
   dotnet ef database update --project WishlistApi
   ```

5. **Run the API:**
   ```bash
   dotnet run --project WishlistApi --launch-profile https
   ```

   The API will be available at:
   - HTTPS: `https://localhost:7290`
   - API Reference (Scalar): `https://localhost:7290/scalar`

### Running Tests

```bash
dotnet test
```

Run tests with coverage:
```bash
dotnet test /p:CollectCoverage=true /p:CoverageReportsDirectory=./coverage
```

## 📐 Architecture

The backend follows **Domain-Driven Design (DDD)** principles with a layered architecture:

### Layers

1. **WishlistApi (Presentation)**
   - API controllers
   - Request/response DTOs
   - Middleware
   - Dependency injection configuration

2. **WishlistApi.Core (Domain)**
   - Domain entities and value objects
   - Business logic and domain services
   - Repository interfaces
   - Domain events

3. **WishlistApi.Data (Infrastructure)**
   - EF Core DbContext
   - Repository implementations
   - Database migrations
   - External service integrations

4. **WishlistApi.Tests**
   - Unit tests for domain logic
   - Integration tests for API endpoints
   - Repository tests

### Key Principles

- ✅ **SOLID principles** throughout
- ✅ **Dependency Injection** for loose coupling
- ✅ **Repository pattern** for data access
- ✅ **Async/await** for all I/O operations
- ✅ **RESTful API design** with proper HTTP verbs and status codes

## 🔐 Security

- JWT Bearer token authentication (to be implemented)
- Role-based authorization
- Input validation with FluentValidation
- HTTPS enforced in production
- Secure configuration management (no secrets in code)

See `.github/security-and-owasp.instructions.md` for detailed security guidelines.

## 📚 API Documentation

API documentation is available via:
- **Scalar**: `https://localhost:7290/scalar` - Modern API reference UI
- **OpenAPI Spec**: `https://localhost:7290/openapi/v1.json` - Raw OpenAPI specification

## 🐛 Debugging

### Visual Studio Code
Press `F5` or use the "Run and Debug" panel with the pre-configured launch settings.

### Visual Studio
Open `WishlistApp.sln` and press `F5`.

### Command Line
```bash
dotnet run --project WishlistApi --launch-profile https
```

## 📊 Development Guidelines

- Follow coding standards in `.github/csharp.instructions.md`
- Follow DDD patterns in `.github/dotnet-architecture-good-practices.instructions.md`
- Maintain minimum 85% test coverage for Core and Data layers
- Use async/await for all I/O-bound operations
- Document public APIs with XML comments

## 🔄 Common Commands

```bash
# Build solution
dotnet build

# Run API in watch mode (auto-reload on changes)
dotnet watch run --project WishlistApi --launch-profile https

# Add a new migration
dotnet ef migrations add <MigrationName> --project WishlistApi.Data --startup-project WishlistApi

# Update database
dotnet ef database update --project WishlistApi

# Test specific project
dotnet test WishlistApi.Tests

# Clean build artifacts
dotnet clean
```

## 📝 Next Steps

- [ ] Implement authentication & authorization
- [ ] Set up Entity Framework Core with SQL Server
- [ ] Create domain entities (User, Wishlist, WishlistItem)
- [ ] Implement repository pattern
- [ ] Add API controllers for wishlists
- [ ] Implement sharing and claiming functionality
- [ ] Add comprehensive unit and integration tests

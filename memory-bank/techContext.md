# Tech Context

## Technology Stack

### Frontend

#### Angular 21+
- **Version**: 21.x (latest stable)
- **Key Features Used**:
  - Standalone components (default, no NgModules)
  - Signals for reactive state management
  - `input()`, `output()`, `viewChild()` functions (Angular 19+ pattern)
  - TypeScript strict mode
  - Angular Router for routing
  - HttpClient for API communication

#### Bootstrap 5.3.8+
- **Version**: ~5.3.8
- **Usage**: 
  - Imported in `src/styles.scss`
  - Grid system for layout
  - Component classes for UI elements
  - Responsive utilities
- **Customization**: Custom SCSS variables can override Bootstrap defaults

#### TypeScript
- **Version**: Latest compatible with Angular 21
- **Configuration**: Strict mode enabled
- **Key Features**:
  - Strong typing for all code
  - Interfaces for data models
  - Type guards for runtime checks

#### Build Tools
- **Angular CLI**: Project scaffolding, build, serve, test
- **npm**: Package management
- **esbuild**: Bundler (Angular's default in v17+)

### Backend

#### ASP.NET Core 10
- **Version**: 10.0 (latest)
- **Project Type**: Web API with controllers
- **Key Features**:
  - Minimal hosting model
  - Built-in dependency injection
  - Middleware pipeline
  - Configuration system
  - Logging abstractions

#### Entity Framework Core 10
- **Version**: 10.0
- **Provider**: SQL Server
- **Key Features**:
  - Code-first migrations
  - LINQ queries
  - Change tracking
  - Connection resiliency

#### SQL Server
- **Version**: SQL Server Express (free)
- **Alternative**: LocalDB for development
- **Features Used**:
  - Relational tables
  - Foreign key constraints
  - Indexes for performance
  - Transactions

#### Testing
- **xUnit**: Unit testing framework
- **Moq**: Mocking framework (to be added)
- **FluentAssertions**: Assertion library (to be added)

### Development Environment

#### IDE Options
- **Visual Studio Code** (recommended for cross-platform)
  - Extensions: C# Dev Kit, Angular Language Service
- **Visual Studio 2022** (Windows)
- **JetBrains Rider** (alternative)

#### Version Control
- **Git**: Version control
- **GitHub**: Repository hosting
- **.gitignore**: Configured for .NET and Node

#### Command Line Tools
- **dotnet CLI**: .NET project management
- **Angular CLI (ng)**: Angular project management
- **npm**: Node package management

### Configuration Management

#### Backend Configuration
- **appsettings.json**: Base configuration (committed)
- **appsettings.Development.json**: Dev overrides (gitignored)
- **Environment variables**: Secrets and production settings
- **User secrets**: Local development secrets

Example structure:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "..."
  },
  "Jwt": {
    "Key": "...",
    "Issuer": "...",
    "Audience": "..."
  },
  "Logging": { ... }
}
```

#### Frontend Configuration
- **environment.ts**: Development settings
- **environment.prod.ts**: Production settings
- **Angular CLI environments**: Replaced during build

### Dependency Management

#### Backend Packages (Initial)
- `Microsoft.AspNetCore.OpenApi`
- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Tools`
- `Microsoft.AspNetCore.Authentication.JwtBearer` (to be added)
- `FluentValidation` (to be added)
- `Serilog` (to be added)

#### Frontend Packages (Initial)
- `@angular/core`, `@angular/common`, etc.
- `bootstrap`: ~5.3.8
- `rxjs`: Observable library
- `tslib`: TypeScript helpers
- `zone.js`: Angular change detection

### Development Workflow

#### Backend Development
```bash
# Restore dependencies
dotnet restore

# Build solution
dotnet build

# Run API with hot reload
dotnet watch run --project WishlistApi

# Run tests
dotnet test

# Create migration
dotnet ef migrations add MigrationName --project WishlistApi.Data --startup-project WishlistApi

# Update database
dotnet ef database update --project WishlistApi
```

#### Frontend Development
```bash
# Install dependencies
npm install

# Development server
ng serve

# Build for production
ng build

# Run tests
ng test

# Run tests with coverage
ng test --code-coverage

# Generate component
ng generate component features/wishlists/wishlist-list

# Generate service
ng generate service core/services/wishlist
```

### Build & Deployment

#### Development Build
- **Backend**: Debug configuration, verbose logging
- **Frontend**: Development mode, source maps

#### Production Build
- **Backend**: Release configuration, optimized
  ```bash
  dotnet publish -c Release
  ```
- **Frontend**: Production mode, AOT compilation, minification
  ```bash
  ng build --configuration production
  ```

### Database Management

#### Migrations
- EF Core code-first migrations
- Stored in `WishlistApi.Data/Migrations/`
- Applied automatically on startup (development) or manually (production)

#### Connection Strings
- **LocalDB**: `Server=(localdb)\\mssqllocaldb;Database=WishlistAppDb;Trusted_Connection=true;`
- **SQL Express**: `Server=.\\SQLEXPRESS;Database=WishlistAppDb;Trusted_Connection=true;`
- **Production**: Environment variables with encrypted connections

### Testing Strategy

#### Backend Testing
- **Unit Tests**: Test domain logic in isolation
- **Integration Tests**: Test API endpoints with test database
- **Coverage Target**: 85% for Core and Data layers

#### Frontend Testing
- **Unit Tests**: Component and service tests with Jasmine/Karma
- **E2E Tests**: End-to-end tests with Playwright (to be configured)
- **Coverage Target**: 80% for business logic

### Security Considerations

#### Backend
- HTTPS enforced
- CORS configured for frontend origin
- JWT authentication
- Input validation
- SQL injection prevention (EF Core parameterized queries)

#### Frontend
- XSS prevention (Angular sanitization)
- CSRF protection (tokens)
- HttpOnly cookies for JWT storage
- CSP headers

### Performance Optimization

#### Backend
- Async/await for I/O
- Response caching
- Database query optimization
- Connection pooling

#### Frontend
- Lazy loading modules
- OnPush change detection
- Image optimization
- Bundle size optimization

### Monitoring & Logging

#### Backend (Planned)
- Serilog for structured logging
- Application Insights (optional)
- Health check endpoints

#### Frontend (Planned)
- Console logging (development)
- Error reporting service
- Performance monitoring

### Documentation

#### Code Documentation
- XML doc comments for public APIs (backend)
- JSDoc comments for complex logic (frontend)
- README files in each major folder
- Inline comments following `.github/self-explanatory-code-commenting.instructions.md`

#### API Documentation
- Swagger/OpenAPI for API endpoints
- Scalar for modern API docs UI

### System Requirements

#### Development
- **OS**: Windows, macOS, or Linux
- **RAM**: 8GB minimum, 16GB recommended
- **Disk**: 10GB free space
- **.NET SDK**: 10.0+
- **Node.js**: 20.x+
- **SQL Server**: Express or LocalDB

#### Production (Self-Hosted)
- **OS**: Windows Server or Linux
- **RAM**: 4GB minimum
- **Disk**: 20GB minimum
- **.NET Runtime**: 10.0
- **SQL Server**: Express or Standard
- **Reverse Proxy**: IIS or Nginx

### Coding Standards & Guidelines

All code follows standards defined in:
- `.github/angular.instructions.md` - Angular patterns
- `.github/aspnet-rest-apis.instructions.md` - ASP.NET Core patterns
- `.github/csharp.instructions.md` - C# coding standards
- `.github/dotnet-architecture-good-practices.instructions.md` - DDD patterns
- `.github/a11y.instructions.md` - Accessibility requirements
- `.github/security-and-owasp.instructions.md` - Security guidelines
- `.github/self-explanatory-code-commenting.instructions.md` - Comment guidelines

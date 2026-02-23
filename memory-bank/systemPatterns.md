# System Patterns

## Architecture Overview

```
┌─────────────────────────────────────────────────────────────┐
│                         Frontend (Angular)                   │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐      │
│  │  Components  │  │   Services   │  │    Guards    │      │
│  └──────────────┘  └──────────────┘  └──────────────┘      │
└─────────────────────────────────────────────────────────────┘
                            │
                        HTTPS/REST
                            │
┌─────────────────────────────────────────────────────────────┐
│                    Backend (ASP.NET Core)                    │
│  ┌──────────────────────────────────────────────────────┐   │
│  │              Presentation Layer (API)                │   │
│  │    Controllers, Filters, Middleware, DTOs            │   │
│  └──────────────────────────────────────────────────────┘   │
│                            │                                 │
│  ┌──────────────────────────────────────────────────────┐   │
│  │           Application Layer (Use Cases)              │   │
│  │       Command/Query Handlers, Validators             │   │
│  └──────────────────────────────────────────────────────┘   │
│                            │                                 │
│  ┌──────────────────────────────────────────────────────┐   │
│  │          Domain Layer (Core Business Logic)          │   │
│  │    Entities, Value Objects, Domain Services          │   │
│  └──────────────────────────────────────────────────────┘   │
│                            │                                 │
│  ┌──────────────────────────────────────────────────────┐   │
│  │          Infrastructure Layer (Data Access)          │   │
│  │    Repositories, DbContext, External Services        │   │
│  └──────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────┘
                            │
                      Entity Framework
                            │
┌─────────────────────────────────────────────────────────────┐
│                    SQL Server Database                       │
└─────────────────────────────────────────────────────────────┘
```

## Backend Patterns

### Domain-Driven Design (DDD)

#### Aggregates
Primary aggregates in the system:
- **User** - User account and authentication
- **Wishlist** - Wishlist root with items as children
- **WishlistMember** - Relationship between users and wishlists
- **WishlistInvitation** - Invitation management

#### Value Objects
- Email address
- Money (price with currency)
- DateRange (for events)
- InvitationToken

#### Domain Events (Planned)
- `WishlistCreated`
- `WishlistItemAdded`
- `WishlistItemClaimed`
- `WishlistShared`
- `InvitationAccepted`

### Layered Architecture

#### 1. WishlistApi (Presentation)
- Controllers handle HTTP requests/responses
- DTOs for data transfer
- Filters for cross-cutting concerns
- Middleware for authentication, errors, logging

#### 2. WishlistApi.Core (Domain)
- Pure business logic
- No dependencies on infrastructure
- Rich domain models with behavior
- Domain services for multi-aggregate operations
- Interfaces for repositories

#### 3. WishlistApi.Data (Infrastructure)
- EF Core DbContext
- Repository implementations
- Database migrations
- External service integrations (email, etc.)

#### 4. WishlistApi.Tests
- Unit tests for domain logic
- Integration tests for API endpoints
- Test fixtures and helpers

### Repository Pattern

```csharp
// Interface in Core
public interface IWishlistRepository
{
    Task<Wishlist> GetByIdAsync(Guid id);
    Task<IEnumerable<Wishlist>> GetByOwnerIdAsync(Guid ownerId);
    Task AddAsync(Wishlist wishlist);
    Task UpdateAsync(Wishlist wishlist);
    Task DeleteAsync(Guid id);
}

// Implementation in Data
public class WishlistRepository : IWishlistRepository
{
    private readonly ApplicationDbContext _context;
    // Implementation using EF Core
}
```

### Dependency Injection

All dependencies injected via constructor:
```csharp
public class WishlistController : ControllerBase
{
    private readonly IWishlistRepository _wishlistRepository;
    private readonly ILogger<WishlistController> _logger;

    public WishlistController(
        IWishlistRepository wishlistRepository,
        ILogger<WishlistController> logger)
    {
        _wishlistRepository = wishlistRepository;
        _logger = logger;
    }
}
```

## Frontend Patterns

### Angular Architecture

#### Feature-Based Structure
```
app/
├── core/                    # Singleton services, guards
│   ├── services/
│   │   ├── auth.service.ts
│   │   └── api.service.ts
│   ├── guards/
│   │   └── auth.guard.ts
│   └── interceptors/
│       └── auth.interceptor.ts
├── shared/                  # Shared components, directives
│   ├── components/
│   └── pipes/
└── features/                # Feature modules
    ├── auth/
    ├── wishlists/
    └── items/
```

#### State Management with Signals

```typescript
// Service with signal-based state
@Injectable({ providedIn: 'root' })
export class WishlistStateService {
  private wishlistsSignal = signal<Wishlist[]>([]);
  private loadingSignal = signal<boolean>(false);
  
  // Read-only exposed signals
  wishlists = this.wishlistsSignal.asReadonly();
  loading = this.loadingSignal.asReadonly();
  
  // Computed derived state
  wishlistCount = computed(() => this.wishlists().length);
  
  // Methods to update state
  setWishlists(wishlists: Wishlist[]) {
    this.wishlistsSignal.set(wishlists);
  }
  
  addWishlist(wishlist: Wishlist) {
    this.wishlistsSignal.update(lists => [...lists, wishlist]);
  }
}
```

#### Smart vs Presentational Components

**Smart Components** (Container Components):
- Inject services
- Manage state
- Handle business logic
- Pass data to presentational components

**Presentational Components**:
- Receive data via `input()`
- Emit events via `output()`
- No service dependencies
- Purely focused on display

### API Communication

```typescript
@Injectable({ providedIn: 'root' })
export class WishlistApiService {
  private http = inject(HttpClient);
  private apiUrl = environment.apiUrl;
  
  getWishlists(): Observable<Wishlist[]> {
    return this.http.get<Wishlist[]>(`${this.apiUrl}/wishlists`);
  }
  
  createWishlist(data: CreateWishlistDto): Observable<Wishlist> {
    return this.http.post<Wishlist>(`${this.apiUrl}/wishlists`, data);
  }
}
```

## Cross-Cutting Concerns

### Authentication Flow
1. User submits credentials via Angular
2. API validates and returns JWT token
3. Frontend stores token (HttpOnly cookie preferred)
4. HTTP interceptor adds token to all requests
5. API middleware validates token on each request

### Error Handling

**Backend**:
- Global exception middleware catches all errors
- Returns RFC 9457 Problem Details format
- Logs errors with correlation IDs

**Frontend**:
- HTTP interceptor catches API errors
- Global error handler for unexpected errors
- User-friendly error messages in UI

### Logging

**Backend**:
- Structured logging with Serilog
- Log levels: Trace, Debug, Information, Warning, Error, Critical
- Correlation IDs for request tracking

**Frontend**:
- Console logging in development
- Centralized logging service
- Error reporting to backend (future)

## Testing Patterns

### Backend Testing

**Unit Tests**:
```csharp
public class WishlistService_AddItem_Tests
{
    [Fact]
    public void AddItem_ValidItem_AddsToWishlist()
    {
        // Arrange
        var wishlist = new Wishlist(/*...*/);
        var item = new WishlistItem(/*...*/);
        
        // Act
        wishlist.AddItem(item);
        
        // Assert
        Assert.Contains(item, wishlist.Items);
    }
}
```

**Integration Tests**:
- WebApplicationFactory for API testing
- In-memory database or test database
- Test complete request/response cycle

### Frontend Testing

**Component Tests**:
```typescript
describe('WishlistListComponent', () => {
  it('should display wishlists', () => {
    const fixture = TestBed.createComponent(WishlistListComponent);
    fixture.componentRef.setInput('wishlists', mockWishlists);
    
    const compiled = fixture.nativeElement;
    expect(compiled.querySelectorAll('.wishlist-item').length)
      .toBe(mockWishlists.length);
  });
});
```

## Security Patterns

### Input Validation
- Data annotations on DTOs (backend)
- Reactive form validation (frontend)
- Both client-side AND server-side validation

### SQL Injection Prevention
- Parameterized queries (EF Core handles this)
- Never string concatenation for queries

### XSS Prevention
- Angular's built-in sanitization
- Content Security Policy headers
- Proper output encoding

### Authentication
- JWT Bearer tokens
- HttpOnly cookies for token storage
- Secure, SameSite=Strict cookie attributes

## Performance Patterns

### Backend
- Async/await for all I/O operations
- EF Core query optimization
- Response caching where appropriate
- Pagination for large datasets

### Frontend
- Lazy loading for feature modules
- OnPush change detection
- TrackBy for ngFor loops
- Image optimization (WebP format)

## Key Principles Applied

1. **SOLID Principles** - Throughout backend and frontend
2. **DRY (Don't Repeat Yourself)** - Shared components and services
3. **Separation of Concerns** - Clear layer boundaries
4. **Dependency Inversion** - Depend on abstractions
5. **Single Responsibility** - Each class/component has one job

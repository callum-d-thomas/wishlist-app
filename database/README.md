# Wishlist App - Database

SQL Server database scripts and documentation.

## 📋 Overview

This directory contains database-related files:
- Schema documentation
- Seed data scripts
- Manual migration scripts (if needed)
- Database setup instructions

Entity Framework Core migrations are managed in `backend/WishlistApi.Data/Migrations/`.

## 🗄️ Database Schema

### Planned Tables

#### Users
- `Id` (uniqueidentifier, PK)
- `Email` (nvarchar(256), unique)
- `PasswordHash` (nvarchar(256))
- `FirstName` (nvarchar(100))
- `LastName` (nvarchar(100))
- `CreatedAt` (datetime2)
- `UpdatedAt` (datetime2)

#### Wishlists
- `Id` (uniqueidentifier, PK)
- `OwnerId` (uniqueidentifier, FK → Users)
- `Title` (nvarchar(200))
- `Description` (nvarchar(1000))
- `Occasion` (nvarchar(100))
- `EventDate` (datetime2, nullable)
- `CreatedAt` (datetime2)
- `UpdatedAt` (datetime2)

#### WishlistItems
- `Id` (uniqueidentifier, PK)
- `WishlistId` (uniqueidentifier, FK → Wishlists)
- `Name` (nvarchar(200))
- `Description` (nvarchar(1000))
- `Url` (nvarchar(500), nullable)
- `ImageUrl` (nvarchar(500), nullable)
- `Price` (decimal(18,2), nullable)
- `Priority` (int)
- `IsClaimed` (bit)
- `ClaimedBy` (uniqueidentifier, FK → Users, nullable)
- `ClaimedAt` (datetime2, nullable)
- `CreatedAt` (datetime2)
- `UpdatedAt` (datetime2)

#### WishlistMembers
- `WishlistId` (uniqueidentifier, PK, FK → Wishlists)
- `UserId` (uniqueidentifier, PK, FK → Users)
- `Role` (nvarchar(50)) - "Viewer" or "Contributor"
- `InvitedAt` (datetime2)
- `JoinedAt` (datetime2, nullable)

#### WishlistInvitations
- `Id` (uniqueidentifier, PK)
- `WishlistId` (uniqueidentifier, FK → Wishlists)
- `Email` (nvarchar(256))
- `Token` (nvarchar(256), unique)
- `ExpiresAt` (datetime2)
- `IsAccepted` (bit)
- `CreatedAt` (datetime2)

## 🚀 Setup

### SQL Server Express Installation

1. **Download SQL Server Express:**
   - Visit: https://www.microsoft.com/sql-server/sql-server-downloads
   - Download "Express" edition (free)

2. **Install with default settings:**
   - Select "Basic" installation
   - Note the connection string provided after installation

3. **Install SQL Server Management Studio (optional):**
   - Download SSMS: https://aka.ms/ssmsfullsetup
   - Useful for managing databases visually

### LocalDB (Alternative)

If you prefer LocalDB (simpler, designed for development):

```bash
# Check if LocalDB is installed (comes with Visual Studio)
sqllocaldb info

# Create LocalDB instance
sqllocaldb create "WishlistAppDb" -s

# Connection string:
Server=(localdb)\WishlistAppDb;Database=WishlistApp;Trusted_Connection=true;
```

## 🔄 Migrations

Database migrations are handled by **Entity Framework Core** in the backend project.

### Create Migration

```bash
cd backend
dotnet ef migrations add <MigrationName> --project WishlistApi.Data --startup-project WishlistApi
```

### Apply Migration

```bash
dotnet ef database update --project WishlistApi --startup-project WishlistApi
```

### View Migration SQL

```bash
dotnet ef migrations script --project WishlistApi.Data --startup-project WishlistApi
```

### Rollback Migration

```bash
dotnet ef database update <PreviousMigrationName> --project WishlistApi --startup-project WishlistApi
```

## 📝 Connection Strings

### Development (LocalDB)
```json
"Server=(localdb)\\mssqllocaldb;Database=WishlistAppDb;Trusted_Connection=true;MultipleActiveResultSets=true"
```

### Development (SQL Express)
```json
"Server=.\\SQLEXPRESS;Database=WishlistAppDb;Trusted_Connection=true;MultipleActiveResultSets=true"
```

### Production (Example)
```json
"Server=your-server;Database=WishlistAppDb;User Id=wishlist_user;Password=<secure-password>;Encrypt=true;TrustServerCertificate=false"
```

**Security Note:** Never commit connection strings with credentials to source control. Use environment variables or secrets management.

## 🌱 Seed Data

Seed data for development/testing will be added in `WishlistApi.Data/SeedData/`.

Example seed data:
- Test users
- Sample wishlists
- Example items

Apply seed data:
```bash
dotnet run --project WishlistApi --launch-profile https -- seed
```

## 🔐 Security Considerations

- Use parameterized queries (EF Core does this automatically)
- Never store plain-text passwords (use ASP.NET Core Identity)
- Implement proper access control at the application layer
- Use encrypted connections in production
- Regular backups for production data
- Follow principle of least privilege for database users

## 📊 Indexes

Key indexes to be created (via migrations):
- `Users.Email` - Unique index for login
- `Wishlists.OwnerId` - FK index for user's wishlists
- `WishlistItems.WishlistId` - FK index for items
- `WishlistMembers.UserId` - FK index for member lookups
- `WishlistInvitations.Token` - Unique index for invitation validation

## 🧪 Testing Database

For integration tests, consider:
- Using SQL Server in Docker
- In-memory database provider (for simple tests)
- Separate test database instance

## 📚 Resources

- [EF Core Documentation](https://docs.microsoft.com/ef/core/)
- [SQL Server Documentation](https://docs.microsoft.com/sql/sql-server/)
- [Database Design Best Practices](https://docs.microsoft.com/sql/relational-databases/database-design/)

## 📝 Next Steps

- [ ] Install SQL Server Express or set up LocalDB
- [ ] Configure connection string in backend
- [ ] Create initial migration with core entities
- [ ] Apply migration to create database
- [ ] Add seed data for development
- [ ] Document any custom stored procedures or functions

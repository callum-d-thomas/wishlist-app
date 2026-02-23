# Wishlist App

A web application for managing shared wishlists for private groups, families, and friends. Coordinate gift-giving for special occasions while avoiding duplicate purchases.

## 📋 Features

- **Private Group Wishlists**: Create wishlists and invite specific people to view or contribute
- **Item Claiming**: Members can "claim" items (hidden from others) to prevent duplicate gifts
- **Privacy-First**: Only invited members can access wishlists
- **Self-Hostable**: Designed to run on personal infrastructure with minimal resources

## 🛠️ Tech Stack

### Frontend
- **Angular 21+** with standalone components and signals
- **Bootstrap 5.3.8+** for responsive UI
- **TypeScript** with strict mode

### Backend
- **ASP.NET Core 10** Web API
- **SQL Server Express** for local development
- **Entity Framework Core** for data access

## 📁 Project Structure

```
wishlist-app/
├── backend/                    # ASP.NET Core Web API
│   ├── WishlistApi/           # Main API project
│   ├── WishlistApi.Core/      # Domain models, interfaces
│   ├── WishlistApi.Data/      # EF Core, repositories
│   └── WishlistApi.Tests/     # Unit tests
├── frontend/                   # Angular application
│   └── wishlist-client/       # Angular CLI project
├── database/                   # SQL scripts, migrations
└── .github/                    # GitHub configuration & instructions
```

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Node.js 20+](https://nodejs.org/) and npm
- [Angular CLI](https://angular.dev/tools/cli): `npm install -g @angular/cli`
- [SQL Server Express](https://www.microsoft.com/sql-server/sql-server-downloads) or SQL Server LocalDB

### Backend Setup

```bash
cd backend
dotnet restore
dotnet build
dotnet run --project WishlistApi --launch-profile https
```

The API will be available at `https://localhost:7290` (or configured port).
API documentation is available at `https://localhost:7290/scalar`.

See [backend/README.md](backend/README.md) for detailed backend setup.

### Frontend Setup

```bash
cd frontend/wishlist-client
npm install
ng serve
```

The app will be available at `http://localhost:4200`.

See [frontend/README.md](frontend/README.md) for detailed frontend setup.

### Database Setup

1. Ensure SQL Server Express is installed and running
2. Update connection string in `backend/WishlistApi/appsettings.Development.json`
3. Run migrations (instructions coming soon)

See [database/README.md](database/README.md) for database setup details.

## 🧪 Running Tests

### Backend Tests
```bash
cd backend
dotnet test
```

### Frontend Tests
```bash
cd frontend/wishlist-client
ng test
```

## 📚 Documentation

- **Architecture & Patterns**: See `.github/dotnet-architecture-good-practices.instructions.md`
- **Security Guidelines**: See `.github/security-and-owasp.instructions.md`
- **Accessibility**: See `.github/a11y.instructions.md`
- **Coding Standards**: See `.github/angular.instructions.md` and `.github/csharp.instructions.md`

## 🎯 Development Priorities

1. **Learning Focus**: Opportunity to deepen knowledge of Angular 21+ and ASP.NET Core 10
2. **Cost Efficiency**: Self-hostable on personal infrastructure
3. **Code Quality**: DDD principles, SOLID design, comprehensive testing
4. **Accessibility**: WCAG 2.2 Level AA compliance
5. **Security**: OWASP best practices

## 📝 Current Status

✅ Phase 1: Foundation
✅ Phase 2: Backend Auth + Wishlist API
📋 Phase 3: Frontend Auth

## 📄 License

*License to be determined*

## 🤝 Contributing

This is a personal learning project. Contributions and suggestions are welcome!

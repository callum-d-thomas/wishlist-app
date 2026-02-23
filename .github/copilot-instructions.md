# Wishlist App - Project Instructions

## Project Overview

This is a web application for managing shared wishlists for private groups, families, and friends. The focus is on helping people coordinate gift-giving for special occasions (birthdays, holidays, weddings) while avoiding duplicate purchases.

## Core Purpose

- **Private Group Wishlists**: Users create wishlists and invite specific people to view or contribute
- **Item Claiming**: Members can "claim" items (hidden from others) to prevent duplicate gifts
- **Privacy-First**: Only invited members can access wishlists
- **Low-Cost Hosting**: Designed to run on self-hosted infrastructure with minimal resources

## Tech Stack

### Frontend
- **Angular 21+**: Using standalone components, signals for state management, and modern Angular features
- **Bootstrap 5.3.8+**: For responsive UI and consistent styling
- **TypeScript**: Strict mode enabled for type safety

### Backend
- **ASP.NET Core 10**: Web API with controllers or minimal APIs
- **SQL Server**: Using SQL Server Express for development (can run locally)
- **Entity Framework Core**: For data access and migrations

### Development Tools
- **Angular CLI**: For scaffolding and build tooling
- **.NET CLI**: For backend project management
- **Git**: Version control

## Project Priorities

1. **Learning Focus**: This project is an opportunity to deepen knowledge of Angular 21+, ASP.NET Core 10, and modern web development patterns
2. **Cost Efficiency**: Self-hostable on personal infrastructure, minimal external dependencies
3. **Code Quality**: Following DDD principles, SOLID design, and comprehensive testing
4. **Accessibility**: WCAG 2.2 Level AA compliance
5. **Security**: OWASP best practices, secure authentication, and data protection

## Project Scope

### In Scope (Current Phase)
- User authentication and authorization
- Create, edit, delete wishlists
- Share wishlists with specific users (private groups)
- Add items to wishlists with details (name, description, link, image, price)
- Claim items (hidden from other users)
- Basic notification system

### Out of Scope (Future Consideration)
- Public/collaborative wishlists
- Price tracking or web scraping
- Mobile native apps (PWA may be considered)
- Social media integration
- Advanced analytics

## Architecture Guidelines

- **Domain-Driven Design**: Clear bounded contexts, aggregates, and domain events
- **Layered Architecture**: Separation between Domain, Application, Infrastructure, and Presentation layers
- **RESTful APIs**: Standard HTTP methods and status codes
- **Angular Feature Modules**: Organized by domain/feature for scalability
- **Responsive Design**: Mobile-first approach using Bootstrap grid system

## Development Standards

All code should follow the specific instructions defined in:
- `angular.instructions.md` - Angular-specific patterns and best practices
- `aspnet-rest-apis.instructions.md` - ASP.NET Core API guidelines
- `csharp.instructions.md` - C# coding standards
- `dotnet-architecture-good-practices.instructions.md` - DDD and architecture patterns
- `a11y.instructions.md` - Accessibility requirements (WCAG 2.2 Level AA)
- `security-and-owasp.instructions.md` - Security best practices
- `self-explanatory-code-commenting.instructions.md` - Code documentation standards
- `memory-bank.instructions.md` - Project knowledge management

## Configuration Notes

- Use environment-specific configuration files (appsettings.json, environment.ts)
- Never commit secrets or connection strings to version control
- Store sensitive configuration in environment variables or secure vaults

## Testing Strategy

- **Unit Tests**: For domain logic, services, and components
- **Integration Tests**: For API endpoints and database operations
- **E2E Tests**: For critical user flows (authentication, wishlist creation, item claiming)
- Target minimum 85% code coverage for domain and application layers

## Current Status

🚧 **Project Setup Phase** - Establishing skeleton structure and foundational components

---

*This file provides overall project context. Specific technical guidelines are maintained in separate `.instructions.md` files.*

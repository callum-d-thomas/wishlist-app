# Wishlist App - Frontend

Angular 21+ client application for the Wishlist App.

## 📁 Project Structure

```
frontend/wishlist-client/
├── src/
│   ├── app/
│   │   ├── core/              # Core services, guards, interceptors
│   │   ├── shared/            # Shared components, directives, pipes
│   │   ├── features/          # Feature modules (wishlists, auth, etc)
│   │   ├── app.ts             # Root component
│   │   ├── app.routes.ts      # Routing configuration
│   │   └── app.config.ts      # App configuration & providers
│   ├── assets/                # Static assets
│   ├── styles.scss            # Global styles
│   └── index.html             # Main HTML file
├── public/                    # Public assets
├── angular.json               # Angular CLI configuration
└── package.json               # Dependencies
```

## 🛠️ Built With

- **Angular 21+** - Framework with standalone components
- **Bootstrap 5.3.8+** - UI component library
- **TypeScript** - Type-safe JavaScript
- **SCSS** - CSS preprocessor
- **Angular Signals** - Reactive state management
- **Angular Router** - Client-side routing

## 🚀 Getting Started

### Prerequisites

- [Node.js 20+](https://nodejs.org/)
- [Angular CLI](https://angular.dev/tools/cli): `npm install -g @angular/cli`

### Installation

1. **Navigate to the frontend directory:**
   ```bash
   cd frontend/wishlist-client
   ```

2. **Install dependencies:**
   ```bash
   npm install
   ```

3. **Configure API endpoint:**
   
   Update `src/environments/environment.ts` (to be created):
   ```typescript
   export const environment = {
     production: false,
     apiUrl: 'https://localhost:5001/api'
   };
   ```

4. **Run the development server:**
   ```bash
   ng serve
   ```

   Navigate to `http://localhost:4200/`. The app will automatically reload if you change any source files.

### Available Scripts

```bash
# Development server
ng serve

# Development server with open browser
ng serve --open

# Build for production
ng build

# Run unit tests
ng test

# Run end-to-end tests
ng e2e

# Lint code
ng lint

# Generate component
ng generate component features/wishlists/wishlist-list

# Generate service
ng generate service core/services/wishlist
```

## 🎨 Styling

The app uses **Bootstrap 5.3.8+** for UI components and layout. Bootstrap is configured in:
- `src/styles.scss` - Global Bootstrap import
- Component-specific SCSS files for custom styling

### Using Bootstrap

```html
<div class="container">
  <div class="row">
    <div class="col-md-6">
      <button class="btn btn-primary">Click Me</button>
    </div>
  </div>
</div>
```

### Custom Theming

Create theme variables in `src/styles/_variables.scss` (to be created) and import before Bootstrap.

## 📐 Architecture

The frontend follows **Angular best practices** with feature-based organization:

### Structure Guidelines

1. **Core Module** (`app/core/`)
   - Services used across the app (auth, http, state)
   - HTTP interceptors
   - Route guards
   - App-wide providers

2. **Shared Module** (`app/shared/`)
   - Reusable components (modals, forms, buttons)
   - Common directives and pipes
   - Utility functions

3. **Feature Modules** (`app/features/`)
   - Organized by domain (wishlists, auth, user-profile)
   - Each feature is self-contained with its own routing
   - Lazy-loaded where appropriate

### State Management

Using **Angular Signals** for reactive state:

```typescript
// Service with signals
export class WishlistService {
  private wishlistsSignal = signal<Wishlist[]>([]);
  wishlists = this.wishlistsSignal.asReadonly();

  addWishlist(wishlist: Wishlist) {
    this.wishlistsSignal.update(lists => [...lists, wishlist]);
  }
}
```

## 🔐 Security

- JWT tokens stored securely (HttpOnly cookies preferred)
- HTTP interceptors for authentication
- Route guards for protected pages
- XSS protection via Angular's built-in sanitization
- CSRF protection

See `.github/security-and-owasp.instructions.md` for detailed security guidelines.

## ♿ Accessibility

This app follows **WCAG 2.2 Level AA** standards:
- Semantic HTML
- ARIA attributes where needed
- Keyboard navigation support
- Focus management
- Skip links
- Proper color contrast

See `.github/a11y.instructions.md` for detailed accessibility guidelines.

## 🧪 Testing

### Unit Tests
```bash
ng test
```

Unit tests use **Jasmine** and **Karma**.

### E2E Tests
```bash
ng e2e
```

E2E tests will use **Playwright** (to be configured).

### Test Coverage
```bash
ng test --code-coverage
```

Coverage reports are generated in `coverage/` directory.

## 📱 Responsive Design

The app is mobile-first and responsive:
- Bootstrap grid system for layout
- Responsive breakpoints: XS, SM, MD, LG, XL, XXL
- Mobile navigation patterns
- Touch-friendly UI elements

## 🌐 API Integration

API calls are made through services in `app/core/services/`:

```typescript
export class WishlistApiService {
  private apiUrl = inject(ENVIRONMENT).apiUrl;
  private http = inject(HttpClient);

  getWishlists(): Observable<Wishlist[]> {
    return this.http.get<Wishlist[]>(`${this.apiUrl}/wishlists`);
  }
}
```

## 🔄 Development Workflow

1. **Create feature branch** from `main`
2. **Generate components/services** using Angular CLI
3. **Write tests** alongside implementation
4. **Lint and format** code before committing
5. **Test locally** before pushing
6. **Create pull request** for review

## 📝 Coding Standards

- Follow Angular Style Guide: https://angular.dev/style-guide
- Use standalone components (default in Angular 21+)
- Prefer signals over RxJS where appropriate
- Use `input()`, `output()`, `viewChild()` functions (Angular 19+)
- Write self-documenting code (see `.github/self-explanatory-code-commenting.instructions.md`)
- Follow TypeScript strict mode
- Use `OnPush` change detection for performance

## 🐛 Debugging

### VS Code
1. Install "Angular Language Service" extension
2. Use built-in launch configurations
3. Set breakpoints in TypeScript files

### Browser DevTools
- Install Angular DevTools extension for Chrome/Edge
- Use for component inspection and performance profiling

## 📊 Performance

- Lazy load feature modules
- Use `OnPush` change detection strategy
- Optimize images (use WebP format)
- Tree-shakeable providers
- Minimize bundle size (check `ng build` output)

## 📚 Resources

- [Angular Documentation](https://angular.dev)
- [Bootstrap Documentation](https://getbootstrap.com/docs/5.3/)
- [TypeScript Handbook](https://www.typescriptlang.org/docs/)
- [Angular Signals Guide](https://angular.dev/guide/signals)

## 📝 Next Steps

- [ ] Set up environment configuration
- [ ] Create core services (auth, http)
- [ ] Implement authentication flow
- [ ] Build wishlist feature module
- [ ] Create shared UI components
- [ ] Set up E2E testing with Playwright
- [ ] Configure PWA (optional)

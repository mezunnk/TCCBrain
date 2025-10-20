# Copilot Instructions for BrainFlow (TCCBrain)

## Project Overview
- **BrainFlow** is a layered .NET 9.0 web platform for online courses, featuring affiliate management, analytics, and a modern UI.
- Main solution: `BrainFlow.sln`.
- Key layers:
  - `BrainFlow.Repository/`: Data access (EF Core, context, repositories, interfaces)
  - `BrainFlow.UI.Web/`: Web UI, controllers, views, viewmodels, static assets
  - `BrainFlow.Repository/Database/`: SQL scripts, seed data, DB docs

## Architecture & Patterns
- **Layered architecture**: Repository pattern for data access, separation of concerns between UI, business logic, and persistence.
- **Entity Framework Core** is used for ORM/database migrations. Migrations are in `BrainFlow.Repository/Migrations/`.
- **ViewModels** in `BrainFlow.UI.Web/ViewModels/` are used for controller-to-view data transfer.
- **Controllers** in `BrainFlow.UI.Web/Controllers/` handle routing and business logic.
- **Custom Middleware**: See `BrainFlow.UI.Web/Middleware/UserInfoMiddleware.cs` for user context handling.
- **Authentication**: Uses BCrypt.Net-Next for password hashing. See `SISTEMA_AUTENTICACAO_COMPLETO.md` for details.

## Developer Workflows
- **Build & Run**:
  - Restore: `dotnet restore`
  - DB migration: `dotnet ef database update --project BrainFlow.Repository`
  - Run: `dotnet run --project BrainFlow.UI.Web`
- **Database setup**:
  - For MySQL/MariaDB: Run scripts in `BrainFlow.Repository/Database/Scripts/` and seed data in `SeedData/`
  - Default admin: `admin@brainflow.com` / `password` (change in production)
- **Frontend**: Static assets in `wwwroot/`, custom CSS variables in `:root` (see README for palette)

## Conventions & Integration
- **Naming**: Suffixes like `MOD` for models, `REP` for repositories/interfaces, `ViewMOD` for viewmodels.
- **Routes**: `/admin/*` for admin, `/afiliado/*` for affiliates, `/curso/{id}` for course details.
- **Testing**: No explicit test project found; manual testing via web UI and DB recommended.
- **External dependencies**: .NET 9.0, EF Core 9.0.9, BCrypt.Net-Next 4.0.3, MySQL/MariaDB or SQL Server.

## Key Files & Directories
- `README.md`: Project goals, architecture, setup, and workflows
- `BrainFlow.Repository/Context/BrainFlowContext.cs`: EF Core DB context
- `BrainFlow.Repository/Repositories/`: Data access implementations
- `BrainFlow.UI.Web/Controllers/`: Main business logic endpoints
- `BrainFlow.UI.Web/ViewModels/`: Data transfer objects for views
- `BrainFlow.Repository/Database/`: SQL scripts and DB docs
- `setup_database.bat` / `.ps1`: DB setup automation

## Tips for AI Agents
- Follow the layered structure and naming conventions for new features.
- Use EF Core migrations for DB changes; do not edit SQL directly unless updating seed/scripts.
- Reference existing controllers and viewmodels for new endpoints/pages.
- Always update documentation in `README.md` and relevant markdown files for major changes.

---
_If any section is unclear or missing, please request feedback to improve these instructions._

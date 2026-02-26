# RecipeApp

RecipeApp is a Razor Pages web application for managing recipes, categories, pantry items, reviews, and users. The project targets .NET 9 and uses Entity Framework Core with a repository pattern for data access.

## Features

- Create, edit, delete, and view recipes
- Categorize recipes and manage ingredients
- Track pantry items per user
- Post reviews for recipes
- EF Core Code-First migrations included

## Tech Stack

- .NET 9
- ASP.NET Core Razor Pages
- Entity Framework Core (Code First)
- Repository pattern for data access
- SQL Server (LocalDB) or SQLite (configurable via `appsettings.json`)

## Prerequisites

- .NET 9 SDK
- SQL Server (LocalDB) or SQLite for local development
- Visual Studio 2022/2023 or Visual Studio Code (recommended)

## Quick Start (Local)

1. Clone the repository:

   ```bash
   git clone https://github.com/Nogiback/RecipeApp.git
   cd RecipeApp
   ```

2. Edit the connection string in `RecipeApp/appsettings.json` to point to your database. Example (LocalDB):

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=RecipeAppDb;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

Or use SQLite:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Data Source=recipeapp.db"
   }
   ```

3. Restore packages:

   ```bash
   dotnet restore
   ```

4. (Optional) Install EF Core tools if you don't have them:

   ```bash
   dotnet tool install --global dotnet-ef
   ```

5. Apply migrations to create the database:

   ```bash
   dotnet ef database update --project RecipeApp
   ```

A baseline migration exists under `RecipeApp/Migrations`.

6. Run the application:

   ```bash
   dotnet run --project RecipeApp
   ```

Visit the URL displayed in the console (typically `https://localhost:5001`).

## Project Structure (High Level)

- `RecipeApp/Models` — domain models (Recipe, Category, Ingredient, PantryItem, Review, AppUser)
- `RecipeApp/Views` — Razor views and shared layout
- `RecipeApp/Controllers` — controllers used by the app
- `RecipeApp/Repository` — repository interfaces and implementations
- `RecipeApp/Data` — `AppDbContext` and EF Core migrations
- `RecipeApp/ViewModels` — view models used by pages

## Project Team
- Peter Do
- Nguyen Hai Anh Tran
- Bowale Omode

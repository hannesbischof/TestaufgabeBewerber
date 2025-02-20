## Project Description
This project is a full-stack application consisting of:
- **Backend**: An ASP.NET Core Web API for managing products and categories, including CRUD operations, pagination, and business logic validation.
- **Frontend**: A Blazor WebAssembly application that serves as the user interface for interacting with the backend API.

## Prerequisites
- .NET 6 SDK or later
- SQLite (for database)

## Setup Instructions
1. Clone the repository:
   ```bash
   git clone https://github.com/hannesbischof/TestaufgabeBewerber.git
   ```
2. Navigate to the project directory:
   ```bash
   cd TestaufgabeBewerber
   ```
3. Build the project:
   ```bash
   dotnet build
   ```
4. Run the backend application:
   ```bash
   dotnet run --project src/Backend/Program.cs
   ```
5. Run the frontend application:
   ```bash
   cd src/Frontend
   dotnet run
   ```

## Project Structure
```
TestaufgabeBewerber/
├── README.md
├── src/
│   ├── Backend/
│   │   ├── Program.cs                # Entry point of the backend application
│   │   ├── AppDbContext.cs           # EF Core database context
│   │   ├── Models/                   # Contains Product and Category models
│   │   ├── Repositories/             # Data access layer
│   │   ├── Services/                 # Business logic layer
│   │   ├── Controllers/              # API endpoints
│   ├── Frontend/
│   │   ├── BlazorApp.csproj          # Blazor WebAssembly project file
│   │   ├── Program.cs                # Entry point of the frontend application
│   │   ├── App.razor                 # Root component for routing
│   │   ├── Pages/                    # Contains Razor pages (e.g., Index, Login)
```

## Features
- Product and Category management
- Pagination support for listing endpoints
- Business logic validations
- SQLite database integration
- Swagger/OpenAPI documentation
- Blazor WebAssembly frontend for user interaction
- Blazor WebAssembly frontend now includes full CRUD operations for Products and Categories, allowing listing, creation, update, and deletion. New pages are available at `/products` and `/categories`.
- Authentication and Authorization:
  - Only authenticated users (via ASP.NET Core Identity) can create, update, or delete products and categories.
- Updated Business Rules:
  - Products must have a description with a minimum of 10 characters.
  - Categories must have a description with a maximum of 200 characters.
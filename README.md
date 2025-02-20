
## Project Description
This project is an ASP.NET Core Web API application designed to manage products and categories. It includes features such as CRUD operations, pagination, and business logic validation.

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
4. Run the application:
   ```bash
   dotnet run --project src/Backend/Program.cs
   ```

## Project Structure
```
TestaufgabeBewerber/
├── README.md
├── src/
│   ├── Backend/
│   │   ├── Program.cs                # Entry point of the application
│   │   ├── AppDbContext.cs           # EF Core database context
│   │   ├── Models/                   # Contains Product and Category models
│   │   ├── Repositories/             # Data access layer
│   │   ├── Services/                 # Business logic layer
│   │   ├── Controllers/              # API endpoints
```

## Features
- Product and Category management
- Pagination support for listing endpoints
- Business logic validations
- SQLite database integration
- Swagger/OpenAPI documentation
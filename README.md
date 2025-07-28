# EyeScenceApp

EyeScenceApp is a robust, modular .NET application designed to manage and serve digital content related to movies, series, celebrities, awards, and associated media. Built with a clean architecture approach, the project emphasizes maintainability, scalability, and clear separation of concerns across its layers.

## Table of Contents
- [Architecture Overview](#architecture-overview)
- [Key Features](#key-features)
- [Getting Started](#getting-started)
- [Folder Structure](#folder-structure)
- [Contributing](#contributing)
- [License](#license)

## Architecture Overview

The solution is organized into four primary projects, each fulfilling a distinct role:

- **EyeScenceApp.API**  
  Exposes RESTful endpoints for client interaction. This project contains controllers, middleware, helpers, and configuration files to manage HTTP requests and responses.

- **EyeScenceApp.Application**  
  Houses the business logic and application services. It includes service interfaces, implementations, data transfer objects (DTOs), validation logic, and mapping configurations.

- **EyeScenceApp.Domain**  
  Defines the core business entities, enums, repository interfaces, and domain-specific logic. This layer encapsulates the application's business model and rules.

- **EyeScenceApp.Infrastructure**  
  Manages data persistence and external integrations. It contains the database context, entity configurations, migrations, and repository implementations.

## Key Features

- **Comprehensive Digital Content Management:**  
  Manage movies, series, documentaries, and other digital media assets.

- **Celebrity & Crew Management:**  
  Track actors, directors, producers, editors, sound designers, writers, and other crew members.

- **Awards Tracking:**  
  Record and associate awards with celebrities and digital content.

- **User Authentication & Authorization:**  
  Secure user management, including registration, login, and role-based access control.

- **Favorites & Watchlists:**  
  Allow users to curate personal lists of favorite and to-watch content.

- **Ratings & Reviews:**  
  Enable users to rate and review digital content.

- **Genre Categorization & Filtering:**  
  Organize and filter content by genre for enhanced discoverability.

## Getting Started

### Prerequisites
- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later
- A supported database (e.g., SQL Server) configured for the application

### Setup Instructions
1. **Clone the Repository:**
   ```bash
   git clone https://github.com/KyRilloSmaher/EyeSceneApplication
   ```
2. **Open the Solution:**
   Open the solution in Visual Studio or your preferred IDE.
3. **Restore Dependencies:**
   Restore NuGet packages to ensure all dependencies are available.
4. **Build the Solution:**
   Build the solution to verify that all projects compile successfully.

### Running the API
1. **Configure Settings:**
   Update the connection string and other relevant settings in `EyeScenceApp.API/appsettings.json`.
2. **Start the API Project:**
   Run the `EyeScenceApp.API` project.
3. **Access the API:**
   The API will be available at `https://localhost:{port}` (the port is specified in the launch settings).

## Folder Structure

```
EyeScenceApp/
├── EyeScenceApp.API/           # API project: controllers, middleware, helpers
├── EyeScenceApp.Application/   # Business logic, services, DTOs, validation
├── EyeScenceApp.Domain/        # Core entities, enums, repository interfaces
└── EyeScenceApp.Infrastructure/# Data access, EF Core configs, repositories
```

## Contributing

Contributions are welcome! To contribute:
1. Fork the repository.
2. Create a new branch for your feature or bugfix.
3. Commit your changes with clear messages.
4. Submit a pull request describing your changes and rationale.

Please ensure your code adheres to the project's coding standards and includes appropriate tests where applicable.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

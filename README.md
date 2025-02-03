# Public Transport Tax System (PTTS)

## Overview

The **Public Transport Tax System (PTTS)** is an ASP.NET Core Web API built using **Clean Architecture**, **Domain-Driven Design (DDD)**, **Repository Pattern**, and the **Mediator Pattern**. This project allows users to register their vehicles and pay a tax when they enter a particular local government area, with payments restricted to once per day.

## Tech Stack

- **Backend**: ASP.NET Core 9 (C#)
- **Database**: PostgreSQL
- **Authentication**: Microsoft Identity
- **Architecture**: Clean Architecture, DDD, Repository Pattern, Mediator Pattern

## Installation & Setup

### Database Seeding

To seed the database with a super admin account, call the following API endpoint after setting up the project:

```
POST /api/auth/seed
```

This will create a super admin with the following credentials:

- **Email**: testerzero@gmail.com
- **Password**: Strong@password123

With the super admin account, you can perform high-level administrative tasks such as creating admins, assigning roles, managing tax rates, and more.
To set up the project, follow these steps:

1. **Clone the repository**:

   ```sh
   git clone https://github.com/mjavason/public-transport-tax-system.git
   cd public-transport-tax-system
   ```

2. **Configure Environment Variables**:
   Set up the following environment variables/secrets:

   ```json
   {
     "Email:Smtp:Username": "mail@gmail.com",
     "Email:Smtp:Port": "465",
     "Email:Smtp:Host": "smtp.gmail.com",
     "Email:From": "mail@gmail.com",
     "Email:Smtp:Password": "xxxxxxx",
     "JwtSettings:Key": "randomstringsadsusidfhiusadhf",
     "JwtSettings:Issuer": "YourIssuer",
     "JwtSettings:Audience": "YourAudience"
   }
   ```

3. **Set Up Database Connection**:
   Update `appsettings.json` of the PTTS.API project with your PostgreSQL connection string:

   ```json
   "ConnectionStrings": {
     "Database": "Host=your_host;Database=your_db;Username=your_user;Password=your_password"
   }
   ```

4. **Apply Migrations**:
   Run the following command to update the database schema:

   ```sh
   dotnet ef database update
   ```

5. **Run the API**:
   Navigate to the PTTS.API project and start the application using:
   ```sh
   dotnet run
   ```

## API Documentation

Once the API is running, access the API documentation at:
[https://localhost:5085/api/scalar/v1](https://localhost:5020/api/scalar/v1)

## Features

- **User Authentication & Authorization** (Microsoft Identity + JWT)
- **Vehicle Registration & Management**
- **Daily Tax Payment per Local Government Area**
- **Role-Based Access Control**
- **Error Handling & Logging Middleware**

## Project Structure

```
├── Public Transport Tax System.sln
├── .editorconfig
└── src/
    ├── PTTS.API/               # API Layer (Controllers, Middleware, Filters)
    ├── PTTS.Application/       # Application Layer (Commands, Queries, Services)
    ├── PTTS.Core/              # Domain Layer (Entities, Aggregates, Repositories)
    ├── PTTS.Infrastructure/    # Infrastructure Layer (Database, Repositories, Migrations)
```

## Contribution Guidelines

Contributions are welcome! If you want to contribute:

1. Fork the repository.
2. Create a new branch: `git checkout -b feature-branch`
3. Make your changes and commit: `git commit -m 'Add new feature'`
4. Push to your branch: `git push origin feature-branch`
5. Create a pull request.

## Contact

For any inquiries, feel free to reach out!

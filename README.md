# 🌾 Namaa Agricultural Backend API 🚜

[![Backend Core](https://img.shields.io/badge/.NET-9.0-purple.svg)](#)
[![Architecture](https://img.shields.io/badge/Architecture-Clean_/_CQRS-blue.svg)](#)
[![Status](https://img.shields.io/badge/Status-Active_Development-orange.svg)](#)

Namaa is a modular agricultural management platform designed to digitize and improve collaboration between farmers, traders, investors, and agricultural experts.

The system provides a scalable backend API built with **Clean Architecture + CQRS**, focusing on maintainability, separation of concerns, and real-world backend engineering practices.

> 🚧 **Note:** This project is part of a final-year Computer Engineering graduation project. It is actively under development, but the core architecture, pipelines, and system design are already implemented and stable.

---

# 🚀 Features

- 🔐 Authentication & Authorization (JWT)
- 👨‍🌾 Farmer management system
- 🌾 Agricultural listings marketplace
- ⭐ Farmer rating & review system
- 📦 Product order tracking system
- ☁️ Media upload & management (Cloudinary)
- 🤖 AI agricultural consultation (OpenAI API)
- 🌦️ Weather insights integration (OpenWeatherMap)
- 📧 Email notifications (Brevo SMTP)
- ⚡ Global caching with tag-based invalidation
- 🧠 Centralized error handling system
- 📊 Clean CQRS-based request pipeline

---

# </> Tech Stack

| Layer | Technology |
|------|------------|
| Backend | ASP.NET Core 9 (Web API) |
| ORM | Entity Framework Core 9 |
| Database | PostgreSQL |
| Authentication | JWT Bearer Authentication |
| Validation | FluentValidation |
| Mapping | Manual Mapping (Extension Methods) |
| Architecture | Clean Architecture + CQRS + MediatR |
| Logging | Serilog |
| Cloud Storage | Cloudinary |
| External APIs | OpenAI, OpenWeatherMap |
| Email Service | MailKit + Brevo SMTP |
| Caching | Hybrid Cache with Tag Invalidation |

---

# 🧭 Architecture Overview

The system follows **Clean Architecture** to ensure strict separation between business logic and infrastructure concerns.

### 📌 Request Flow

```
Client Request
     ↓
Namaa.API (Controllers + Middleware)
     ↓
Application Layer (CQRS + MediatR Pipeline)
     ↓
Domain Layer (Business Rules & Entities)
     ↓
Infrastructure Layer (Database + External Services)
```

---

### 🧱 Layers

#### 🟢 Namaa.API
- REST API endpoints
- Global exception handling
- Middleware pipeline

#### 🔵 Namaa.Application
- CQRS Commands & Queries
- MediatR handlers
- FluentValidation rules
- DTOs & mapping logic

#### 🟣 Namaa.Domain
- Core business entities (Farmer, Listing, Order, etc.)
- Value objects
- Domain rules & exceptions

#### 🟠 Namaa.Infrastructure
- EF Core database access
- External integrations (OpenAI, Cloudinary, Weather API)
- Email services (Brevo SMTP)
- JWT authentication services

---

# 🧩 System Design Highlights

- ✔ Fully decoupled Clean Architecture
- ✔ CQRS separation (read/write optimization)
- ✔ Centralized error handling pipeline
- ✔ Tag-based caching invalidation strategy
- ✔ Domain-driven modular structure
- ✔ Extensible integration layer for external services

---

# 📋 Prerequisites

Before running the project:

- .NET 9 SDK
- PostgreSQL (running locally)
- IDE (Visual Studio / Rider / VS Code)
- API keys:
  - OpenAI
  - Cloudinary
  - Brevo SMTP
  - OpenWeatherMap

---

# ⚙️ Configuration

Create or update:

```
Namaa.API/appsettings.Development.json
```

Example configuration:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },

  "AllowedHosts": "*",

  "JwtSettings": {
    "Issuer": "localhost",
    "Audience": "localhost",
    "TokenExpirationInMinutes": 60,
    "Secret": "YOUR_JWT_SECRET_KEY"
  },

  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=NamaaDb;Username=postgres;Password=your_password;"
  },

  "App": {
    "BaseUrl": "https://localhost:7070"
  },

  "Serilog": {
    "Using": ["Serilog.Sinks.Console"],
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning"
      }
    }
  },

  "Cloudinary": {
    "CloudName": "your_cloud_name",
    "ApiKey": "your_api_key",
    "ApiSecret": "your_api_secret"
  },

  "WeatherApi": {
    "OpenWeatherMapKey": "your_api_key"
  },

  "OpenAi": {
    "ApiKey": "your_api_key"
  },

  "Smtp": {
    "SmtpServer": "smtp-relay.brevo.com",
    "Port": 587,
    "Username": "your_username",
    "Password": "your_password",
    "SenderEmail": "your_email",
    "SenderName": "Namaa System"
  }
}
```

---

# 🚀 How to Run

### 1. Clone repository
```bash
git clone https://github.com/YourUsername/Namaa-Backend.git
cd Namaa-Backend
```

### 2. Restore packages
```bash
dotnet restore
```

### 3. Apply migrations
```bash
dotnet ef database update --project src/Namaa.Infrastructure --startup-project src/Namaa.API
```

### 4. Run project
```bash
dotnet run --project src/Namaa.API
```

### 5. Swagger
```
https://localhost:7070/swagger
```

---

# ⭐ Final Note

This project demonstrates:
- real-world backend architecture design
- scalable CQRS implementation
- production-style API structure
- integration with external services
- clean separation of concerns

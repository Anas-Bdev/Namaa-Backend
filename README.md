# 🌾 Namaa Agricultural Backend API 🚜

[![Backend Core](https://img.shields.io/badge/.NET-9.0-purple.svg)](#)
[![Architecture](https://img.shields.io/badge/Architecture-Clean_/_CQRS-blue.svg)](#)
[![Status](https://img.shields.io/badge/Status-Active_Development-orange.svg)](#)

---

# 📌 Project Overview

**Namaa** is a backend agricultural platform designed to modernize and digitize the agricultural ecosystem by connecting farmers, traders, investors, agricultural experts, and administrators through a unified system.

The platform aims to address common challenges in the agricultural sector, including fragmented communication, limited market accessibility, and the lack of centralized digital services. By providing a scalable backend infrastructure, Namaa enables stakeholders to interact through a single platform that supports agricultural trading, consultations, investment opportunities, and intelligent services.

> 🚧 **Project Status:** This project is currently under active development as part of a final-year Computer Engineering graduation project. While some business features are still being expanded, the core architecture, security, caching, validation, and infrastructure layers are fully implemented and operational.

---

# 🌱 Vision

Namaa was created to help digitize agricultural workflows and provide a foundation for modern agricultural services.

Rather than focusing solely on CRUD operations, the project emphasizes:

- Scalable backend architecture
- Maintainable business logic
- Secure API design
- Production-oriented engineering practices
- Integration with external services

---

# 🏗️ Architecture & Design

The project follows **Clean Architecture** principles and is organized into four distinct layers.

### 🧱 Namaa.API

Responsible for:

- Controllers
- Request handling
- Authentication
- ProblemDetails responses
- Exception handling registration
- Swagger/OpenAPI configuration

### ⚙️ Namaa.Application

Responsible for:

- CQRS Commands & Queries
- MediatR handlers
- DTOs
- Business use cases
- Validation rules
- Application contracts

### 🧠 Namaa.Domain

Responsible for:

- Entities
- Value Objects
- Domain Rules
- Enumerations
- Domain Errors

### 🔌 Namaa.Infrastructure

Responsible for:

- Entity Framework Core
- PostgreSQL
- JWT services
- Cloudinary integration
- OpenAI integration
- Weather integration
- Email services
- Caching services

---

# ⚡ Architectural Patterns

### CQRS + MediatR

The system separates read and write operations through CQRS:

- Commands → Mutate state
- Queries → Read data
- MediatR → Request orchestration

This improves maintainability and scalability.

---

### Result Pattern

Business operations return strongly typed results rather than relying on exceptions for flow control.

Benefits:

- Predictable operation outcomes
- Explicit success/failure handling
- Consistent API responses
- Improved maintainability

---

### Typed Error System

The application uses a centralized error model with categorized error types.

Examples include:

- Validation Errors
- Not Found Errors
- Conflict Errors
- Unauthorized Errors
- Forbidden Errors

This enables consistent error propagation throughout the application.

---

### Global Exception Handling

Unhandled exceptions are processed through ASP.NET Core's:

```csharp
IExceptionHandler
```

The exception handler maps exceptions into RFC 7807 Problem Details responses to ensure consistent API behavior.

---

# 🧠 Engineering Practices

The project incorporates several production-oriented backend practices:

- ✅ Clean Architecture
- ✅ CQRS + MediatR
- ✅ Result Pattern
- 🛡️ Centralized exception handling using ASP.NET Core IExceptionHandler
- 🚨 Typed error system with categorized error types
- 📄 RFC 7807 Problem Details responses
- ✔️ FluentValidation
- 🔐 JWT Authentication & Authorization
- ⚡ Hybrid Cache with tag-based invalidation
- 📝 Structured logging using Serilog

---

# 🚀 Features

- 🔐 Authentication & Authorization (JWT)
- 👨‍🌾 Farmer management
- 🌾 Product listings marketplace
- 📦 Order management workflow
- ⭐ Farmer ratings and reviews
- 🤖 AI agricultural assistant (OpenAI)
- 🌦️ Weather information integration
- ☁️ Cloudinary image storage
- 📧 Email verification links
- 🔑 Password reset via OTP
- ⚡ Hybrid caching with tag invalidation
- 📊 CQRS-based architecture
- 📄 RFC 7807 Problem Details responses

---

# 👥 System Roles

| Role | Description |
|--------|--------|
| 👨‍🌾 Farmer | Creates listings, manages products, receives ratings |
| 🛒 Trader | Purchases agricultural products |
| 💰 Investor | Funds agricultural opportunities |
| 🌱 Expert | Provides agricultural consultation |
| 🛠️ Administrator | Manages and moderates the platform |
| 👤 Guest | Public browsing access |

---

# </> Tech Stack

| Layer | Technology |
|---------|---------|
| Backend | ASP.NET Core 9 |
| ORM | Entity Framework Core 9 |
| Database | PostgreSQL |
| Authentication | JWT Bearer Authentication |
| Validation | FluentValidation |
| Architecture | Clean Architecture |
| Patterns | CQRS + MediatR |
| Error Handling | Result Pattern + Typed Errors |
| Exception Handling | ASP.NET Core IExceptionHandler |
| Logging | Serilog |
| Cloud Storage | Cloudinary |
| AI Services | OpenAI API |
| Weather Services | OpenWeatherMap API |
| Email Service | MailKit + Brevo SMTP |
| Caching | Hybrid Cache |

---

# 🧭 Request Flow

```text
Client Request
      │
      ▼
API Controllers
      │
      ▼
MediatR Pipeline
      │
      ├── Validation
      ├── Caching
      └── Logging
      │
      ▼
CQRS Handler
      │
      ▼
Domain Logic
      │
      ▼
Infrastructure Services
      │
      ▼
Database / External Services
```

---

# ⚙️ Configuration

Before running the application, configure the required values inside:

```text
appsettings.json
```

Update the values according to your environment.

## Example Configuration

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=NamaaDb;Username=postgres;Password=your_password;"
  },

  "JwtSettings": {
    "Issuer": "localhost",
    "Audience": "localhost",
    "TokenExpirationInMinutes": 60,
    "Secret": "YOUR_JWT_SECRET_KEY"
  },

  "Cloudinary": {
    "CloudName": "your_cloud_name",
    "ApiKey": "your_api_key",
    "ApiSecret": "your_api_secret"
  },

  "OpenAi": {
    "ApiKey": "your_api_key"
  },

  "WeatherApi": {
    "OpenWeatherMapKey": "your_api_key"
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

# 🚀 Running the Application

### Clone the Repository

```bash
git clone https://github.com/YOUR_USERNAME/Namaa-Backend.git
cd Namaa-Backend
```

### Restore Dependencies

```bash
dotnet restore
```

### Run the Application

Database migrations are automatically applied on startup.

```bash
dotnet run --project src/Namaa.API
```

### Access Swagger

```text
https://localhost:7070/swagger
```

---

# 🧩 System Design Highlights

- ✔️ Clean Architecture with strict separation of concerns
- ⚡ CQRS pattern using MediatR
- ✅ Result Pattern for consistent operation outcomes
- 🛡️ Centralized exception handling using IExceptionHandler
- 🚨 Typed error system with categorized error types
- 📄 RFC 7807 Problem Details responses
- ⚡ Hybrid Cache with tag-based invalidation
- 🔐 JWT Authentication & Authorization
- 🔌 Integration with OpenAI, Cloudinary, OpenWeatherMap, and Brevo
- 🏗️ Production-oriented backend structure

---

# 👥 Contributors

This project was developed as a collaborative graduation engineering project.

- 👨‍💻 **Anas Haj Mohammad** — Software Engineer (Backend Architecture, CQRS, API Design, Core System Implementation)
- 👨‍💻 **Ala'a Abu Musa** — Software Engineer (Backend Development)  
  GitHub: https://github.com/alaaabumusa

Both contributors worked together to design and implement the system as part of their final-year Computer Engineering graduation requirements.

---

# ⭐ Final Note

Namaa is a backend platform built using modern .NET development practices and architectural patterns. The project focuses on scalability, maintainability, clean design, and real-world backend engineering concepts while continuing to evolve as an active graduation project.

# 🌾 Namaa Agricultural Backend API 🚜

![Architecture](https://img.shields.io/badge/Architecture-Clean_/_CQRS-blue?style=flat-square) ![Status](https://img.shields.io/badge/Status-Active_Development-orange?style=flat-square) ![.NET 9](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat-square&logo=dotnet&logoColor=white) ![C#](https://img.shields.io/badge/C%23-Ready-239120?style=flat-square&logo=c-sharp&logoColor=white) ![PostgreSQL](https://img.shields.io/badge/PostgreSQL-Passed-4169E1?style=flat-square&logo=postgresql&logoColor=white) ![OpenAI](https://img.shields.io/badge/OpenAI-Integrated-412991?style=flat-square&logo=openai&logoColor=white)
---

# 📌 Project Overview

**Namaa** is a backend agricultural platform designed to modernize and digitize the agricultural ecosystem by connecting farmers, traders, investors, agricultural experts, and administrators through a unified system.

The platform addresses common challenges in the agricultural sector, including fragmented communication, limited market accessibility, inefficient farm management processes, and the lack of centralized digital services.

Beyond marketplace functionality, Namaa provides agricultural management capabilities that allow farmers to register lands, manage cultivation and seeding cycles, publish agricultural products, interact with experts, receive ratings from traders, and participate in a broader digital agricultural ecosystem.

By providing a scalable backend infrastructure, Namaa enables stakeholders to collaborate through a single platform that supports agricultural trading, farm management, consultations, investment opportunities, weather insights, and AI-powered assistance.

> 🚧 **Project Status:** This project is currently under active development as part of a final-year Computer Engineering graduation project. While some business features are still being expanded, the core architecture, security, validation, caching, integrations, and infrastructure layers are fully implemented and operational.

---

# 🌱 Vision

Namaa was created to help digitize agricultural workflows and provide a foundation for modern agricultural services.

Rather than focusing solely on CRUD operations, the project emphasizes:

- Scalable backend architecture
- Maintainable business logic
- Secure API design
- Production-oriented engineering practices
- Integration with external services
- Real-world agricultural domain modeling

---

# 🏗️ Architecture & Design

The project follows **Clean Architecture** principles and is organized into four distinct layers.

## 🧱 Namaa.API

Responsible for:

- Controllers
- Request handling
- Authentication
- ProblemDetails responses
- Exception handling registration
- Swagger/OpenAPI configuration

## ⚙️ Namaa.Application

Responsible for:

- CQRS Commands & Queries
- MediatR handlers
- DTOs
- Business use cases
- Validation rules
- Application contracts
- Pipeline behaviors

## 🧠 Namaa.Domain

Responsible for:

- Entities
- Value Objects
- Domain Rules
- Enumerations
- Domain Errors
- Business invariants

## 🔌 Namaa.Infrastructure

Responsible for:

- Entity Framework Core
- PostgreSQL
- JWT services
- Cloudinary integration
- OpenAI integration
- OpenWeatherMap integration
- Email services
- Hybrid caching services

---

# ⚡ Architectural Patterns

## CQRS + MediatR

The system separates read and write operations through CQRS:

- Commands → Mutate state
- Queries → Read data
- MediatR → Request orchestration

This improves maintainability, scalability, and separation of concerns.

---

## Result Pattern

Business operations return strongly typed results rather than relying on exceptions for flow control.

Benefits:

- Predictable operation outcomes
- Explicit success/failure handling
- Consistent API responses
- Improved maintainability

---

## Typed Error System

The application uses a centralized error model with categorized error types.

Examples include:

- Validation Errors
- Not Found Errors
- Conflict Errors
- Unauthorized Errors
- Forbidden Errors

This enables consistent error propagation throughout the application.

---

## Global Exception Handling

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
- ⏱️ Request performance monitoring via MediatR pipeline behaviors
- 👤 Active-user authorization checks via MediatR pipeline behaviors

---

# 🚀 Features

## 🔐 Security & Identity

- JWT Authentication & Authorization
- Email verification workflow
- Password reset via OTP
- Active-user authorization pipeline

### Supported Roles

- 👨‍🌾 Farmer
- 🛒 Trader
- 💰 Investor
- 🌱 Agricultural Expert
- 🛠️ Administrator
- 👤 Guest

---

## 👨‍🌾 Agricultural Management

- Farmer profile management
- Agricultural land registration
- Crop and seeding cycle management
- Farm activity tracking
- Agricultural lifecycle monitoring

---

## 🌾 Marketplace & Trading

- Product listing management
- Agricultural marketplace
- Product browsing and discovery
- Order management workflow
- Trader interactions
- Farmer ratings and reviews

---

## 💰 Investment & Consultation

- Agricultural investment opportunities
- Expert consultation services
- AI-powered agricultural assistant

---

## ☁️ Integrations

- Cloudinary media storage
- OpenAI integration
- OpenWeatherMap integration
- Brevo SMTP email services

---

## 🏗️ Engineering Features

- Clean Architecture
- CQRS + MediatR
- Result Pattern
- Typed Error System
- RFC 7807 Problem Details
- ASP.NET Core IExceptionHandler
- FluentValidation
- Hybrid Cache with tag-based invalidation
- Structured logging with Serilog
- Performance monitoring pipeline behavior

---

# 👥 System Roles

| Role | Description |
|--------|--------|
| 👨‍🌾 Farmer | Registers lands, manages cultivation cycles, creates listings, receives ratings, and manages agricultural activities |
| 🛒 Trader | Purchases agricultural products and reviews farmers |
| 💰 Investor | Funds agricultural opportunities |
| 🌱 Expert | Provides agricultural consultation and guidance |
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
| Caching | ASP.NET Core HybridCache |

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
      ├── 🛡️ Unhandled Exception Behavior
      ├── ⏱️ Performance Monitoring Behavior
      ├── 👤 Active User Authorization Behavior
      ├── ⚡ Caching Behavior
      └── ✔️ Validation Behavior
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

Update the values according to your local environment.

---

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

## ⚠️ Important Notes

- 🚧 This project is still under active development
- 🏗️ Core architecture and infrastructure are fully implemented
- 🔐 Do not commit real secrets or API keys
- 🧪 Additional business features are currently being developed

---

# 🚀 Running the Application

## 1. Clone the Repository

```bash
git clone https://github.com/YOUR_USERNAME/Namaa-Backend.git
cd Namaa-Backend
```

## 2. Restore Dependencies

```bash
dotnet restore
```

## 3. Run the Application

Database migrations are automatically applied on startup.

```bash
dotnet run --project src/Namaa.API
```

## 4. Access Swagger

```text
https://localhost:7070/swagger
```

---

# 🧩 System Design Highlights

- ✔️ Clean Architecture with strict separation of concerns
- ⚡ CQRS pattern using MediatR
- ✅ Result Pattern for consistent operation outcomes
- 🛡️ Centralized exception handling using ASP.NET Core IExceptionHandler
- 🚨 Typed error system with categorized error types
- 📄 RFC 7807 Problem Details responses
- ⚡ Hybrid Cache with tag-based invalidation
- 🔐 JWT Authentication & Authorization
- 👤 Active-user authorization pipeline behavior
- ⏱️ Request performance monitoring behavior
- ✔️ FluentValidation pipeline behavior
- 🔌 Integration with OpenAI, Cloudinary, OpenWeatherMap, and Brevo
- 🏗️ Production-oriented backend structure

---

# 👥 Contributors

This project was developed as a collaborative graduation engineering project.

### 👨‍💻 Anas Haj Mohammad
**Software Engineer**

GitHub:
https://github.com/Anas-Bdev

Responsibilities:

- Backend Architecture Design
- Clean Architecture Implementation
- CQRS & MediatR
- API Design
- Domain Modeling
- Caching Strategy
- Validation Pipeline
- Error Handling Architecture
- Core System Implementation

### 👨‍💻 Ala'a Abu Musa
**Software Engineer**

GitHub:
https://github.com/alaaabumusa

Responsibilities:

- Backend Development
- Feature Implementation
- System Collaboration

Both contributors worked together to design and implement the system as part of their final-year Computer Engineering graduation requirements.

---

# ⭐ Final Note

Namaa is a real-world backend platform built using modern .NET development practices and architectural patterns. The project demonstrates scalable system design, clean engineering principles, domain-driven thinking, secure API development, and production-oriented backend architecture while continuing to evolve as an active graduation project.

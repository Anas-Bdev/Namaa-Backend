# 🌾 Namaa Agricultural Backend API 🚜

[![Backend Core](https://img.shields.io/badge/.NET-9.0-purple.svg)](#)
[![Architecture](https://img.shields.io/badge/Architecture-Clean_/_CQRS-blue.svg)](#)
[![Status](https://img.shields.io/badge/Status-Active_Development-orange.svg)](#)

---

# 📌 Project Overview

**Namaa** is a backend agricultural platform designed to modernize and digitize the agricultural ecosystem by connecting farmers, traders, investors, agricultural experts, and administrators in a unified system.

In traditional systems, stakeholders often operate in isolation. Farmers face difficulties accessing markets, traders lack transparency in sourcing, and experts are disconnected from real-time agricultural needs. Namaa was created to help bridge these gaps through a scalable backend platform that supports communication, transactions, and agricultural services.

> 🚧 **Important Note:** This project is currently under active development as part of a final-year Computer Engineering graduation project. The core backend architecture and infrastructure are implemented and functional, while some domain-specific features are still being developed.

---

## 🌱 The Idea Behind the Project

Namaa began as a graduation engineering initiative focused on addressing real-world challenges in the agricultural sector.

Rather than building a simple CRUD application, the objective was to design and implement a backend system using modern software engineering practices, emphasizing maintainability, scalability, and clean architecture principles.

The project serves as both an academic endeavor and a practical exploration of production-style backend development.

---

## 🏗️ How the System Was Built

The application follows modern backend engineering practices and architectural patterns.

### 🧱 Clean Architecture

The solution is organized into four primary layers:

- **Namaa.API** → Controllers, middleware, request handling, and API endpoints
- **Namaa.Application** → CQRS handlers, business logic, DTOs, validation, and application services
- **Namaa.Domain** → Core entities, business rules, value objects, and domain abstractions
- **Namaa.Infrastructure** → Database access, authentication, external integrations, caching, and storage services

This structure promotes separation of concerns and long-term maintainability.

---

### ⚡ CQRS + MediatR

The system follows the Command Query Responsibility Segregation (CQRS) pattern:

- Commands → Write operations
- Queries → Read operations
- MediatR → Request dispatching and pipeline orchestration

This keeps application logic organized and scalable.

---

### 🧠 Engineering Practices

The project incorporates several production-oriented backend practices:

- ✅ Result Pattern for predictable operation outcomes
- 🛡️ Global exception handling middleware
- 🚨 Typed error system with categorized error types
- ✔️ FluentValidation for request validation
- 🔐 JWT authentication & authorization
- ⚡ Tag-based caching strategy
- 📝 Structured logging using Serilog

---

### 🔌 External Integrations

The platform integrates multiple external services:

- 🤖 OpenAI API → Agricultural AI assistant
- 🌦️ OpenWeatherMap API → Weather insights
- ☁️ Cloudinary → Image and media storage
- 📧 Email system → Email verification link + password reset OTP

---

# 🚀 Features

- 🔐 Authentication & Authorization (JWT)
- 👨‍🌾 Farmer management system
- 🌾 Agricultural marketplace (listings)
- ⭐ Farmer rating & review system
- 📦 Order and trading workflow system
- ☁️ Media upload system (Cloudinary)
- 🤖 AI agricultural assistant (OpenAI)
- 🌦️ Weather data integration
- 📧 Account security email system (verification link + password reset OTP)
- ⚡ Hybrid caching with tag-based invalidation
- ✅ Result Pattern–based error handling
- 🛡️ Global exception handling middleware
- 📊 CQRS-based architecture

---

# 👥 System Roles

| Role | Description |
|--------|--------|
| 👨‍🌾 Farmer | Creates listings, manages products, receives ratings |
| 🛒 Trader | Purchases agricultural products |
| 💰 Investor | Funds agricultural opportunities |
| 🌱 Expert | Provides agricultural consultation |
| 🛠️ Admin | System management and moderation |
| 👤 Guest | Public browsing access |

---

# </> Tech Stack

| Layer | Technology |
|--------|--------|
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

The system follows Clean Architecture principles to ensure separation of concerns.

### 📌 Request Flow

```text
Client Request
     ↓
Namaa.API (Controllers + Middleware)
     ↓
Application Layer (CQRS + MediatR Pipeline)
     ↓
Domain Layer (Business Rules)
     ↓
Infrastructure Layer (Database + External Services)
```

---

# ⚙️ Configuration

Before running the application, ensure all required settings are configured in `appsettings.json`.

Modify the values according to your local or development environment.

---

## 📌 Example Configuration

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

## ⚠️ Notes

- 🚧 This project is still under active development
- Core backend architecture is stable and functional
- Some domain-specific features are still in progress
- Never commit real secrets or API keys to source control

---

# 🚀 How to Run

### 1. Clone the Repository

```bash
git clone https://github.com/YourUsername/Namaa-Backend.git
cd Namaa-Backend
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Run the Application

Database migrations are automatically applied on startup.

```bash
dotnet run --project src/Namaa.API
```

### 4. Access Swagger

```text
https://localhost:7070/swagger
```

---

# 🧩 System Design Highlights

- ✔️ Clean Architecture with strict layer separation
- ⚡ CQRS pattern for scalable request handling
- ✅ Result Pattern for consistent error propagation
- 🛡️ Global exception handling middleware
- 🚨 Typed error system with categorized error types
- ⚡ Tag-based caching strategy
- 👥 Multi-role system design
- 🔌 External integrations (AI, Weather, Email)
- 🏗️ Production-style backend structure

---

# 👥 Contributors

This project was developed as a collaborative graduation engineering project.

- 👨‍💻 **Anas Haj Mohammad** — Software Engineer (Backend Architecture, CQRS, API Design, Core System Implementation)
- 👨‍💻 **Ala'a Abu Musa** — Software Engineer (Backend Development)  
  GitHub: https://github.com/alaaabumusa

Both contributors worked together to design and implement the system as part of their final-year Computer Engineering graduation requirements.

---

# ⭐ Final Note

Namaa is a backend system built using modern .NET development practices and architectural patterns. The project emphasizes maintainability, scalability, clean design, and real-world backend engineering concepts while continuing to evolve as a graduation project.

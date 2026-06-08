# 🌾 Namaa Agricultural Backend API 🚜

[![Backend Core](https://img.shields.io/badge/.NET-9.0-purple.svg)](#)
[![Architecture](https://img.shields.io/badge/Architecture-Clean_/_CQRS-blue.svg)](#)
[![Status](https://img.shields.io/badge/Status-Active_Development-orange.svg)](#)

---

# 📌 Project Overview

**Namaa** is a backend agricultural platform designed to modernize and digitize the agricultural ecosystem by connecting farmers, traders, investors, agricultural experts, and administrators in a unified system.

In traditional systems, each stakeholder operates in isolation. Farmers face difficulties accessing markets, traders lack transparency in sourcing, and experts are disconnected from real-time agricultural needs. Namaa was built to solve this fragmentation by introducing a structured backend system that simulates a real-world agricultural marketplace.

> 🚧 **Important Note:** This project is currently under active development as part of a final-year Computer Engineering graduation project. The core backend architecture is stable and fully implemented, while some domain-specific features are still being developed.

---

## 🌱 The Idea Behind the Project

This project started as a graduation engineering initiative aimed at solving real-world inefficiencies in the agricultural sector.

Instead of building a simple CRUD application, the goal was to design a **production-style backend system** using modern software engineering principles.

The focus is on building a **clean, scalable, and maintainable architecture** that reflects real-world backend systems.

---

## 🏗️ How the System Was Built

The system is designed using modern backend engineering principles:

### 🧱 Clean Architecture
The system is structured into four layers:

- **Namaa.API** → Controllers, middleware, request handling
- **Namaa.Application** → Business logic, CQRS, validation, handlers
- **Namaa.Domain** → Core business entities and rules
- **Namaa.Infrastructure** → Database, external APIs, integrations

This ensures strong separation of concerns and long-term maintainability.

---

### ⚡ CQRS + MediatR
The system follows CQRS principles:

- Commands → write operations
- Queries → read operations
- MediatR → request pipeline orchestration

This improves scalability and keeps business logic organized.

---

### 🧠 Engineering Practices
The project includes production-level backend concepts:

- Centralized error handling system
- FluentValidation for request validation
- JWT authentication & authorization
- Tag-based caching strategy
- Structured logging using Serilog

---

### 🔌 External Integrations
The system integrates external services to simulate real production behavior:

- 🤖 OpenAI API → AI agricultural assistant
- 🌦️ OpenWeatherMap API → Weather insights
- ☁️ Cloudinary → Media storage and management
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
- 🧠 Centralized error handling system
- 📊 CQRS-based architecture

---

# 👥 System Roles

| Role | Description |
|------|------------|
| 👨‍🌾 Farmer | Creates listings, manages products, receives ratings |
| 🛒 Trader | Purchases agricultural products |
| 💰 Investor | Funds agricultural opportunities |
| 🌱 Expert | Provides agricultural consultation |
| 🛠️ Admin | System management and moderation |
| 👤 Guest | Public browsing access |

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

The system follows Clean Architecture principles to ensure separation of concerns.

### 📌 Request Flow

```
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

You only need to modify this file with your local or development values.

---

## 📌 Required Settings

- PostgreSQL connection string  
- JWT secret key  
- External API keys (if used)

---

## 📌 Full Example Configuration

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

## ⚠️ Important Note

- 🚧 This project is still under active development  
- Core backend architecture is stable and fully implemented  
- Some domain-specific features are still in progress  
- Do not commit real secrets or API keys  

---

# 🚀 How to Run

### 1. Clone repository
```bash
git clone https://github.com/YourUsername/Namaa-Backend.git
cd Namaa-Backend
```

---

### 2. Restore dependencies
```bash
dotnet restore
```

---

### 3. Run application
The system automatically applies database migrations on startup.

```bash
dotnet run --project src/Namaa.API
```

---

### 4. Access Swagger
```
https://localhost:7070/swagger
```

---

# 🧩 System Design Highlights

- ✔ Clean Architecture with strict layer separation  
- ✔ CQRS pattern for scalable request handling  
- ✔ Centralized error handling pipeline  
- ✔ Tag-based caching strategy  
- ✔ Multi-role system design  
- ✔ External integrations (AI, Weather, Email)  
- ✔ Production-style backend structure  

---

# ⭐ Final Note

Namaa is a real-world backend system built using modern .NET architecture principles. It demonstrates scalable system design, clean engineering practices, and production-ready backend development suitable for real industry applications, while still being actively developed as a graduation project.

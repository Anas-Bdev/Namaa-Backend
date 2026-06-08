# 🌾 Namaa Agricultural Backend API 🚜

[![Backend Core](https://img.shields.io/badge/.NET-9.0-purple.svg)](#)
[![Architecture](https://img.shields.io/badge/Architecture-Clean_/_CQRS-blue.svg)](#)
[![Status](https://img.shields.io/badge/Status-Active_Development-orange.svg)](#)

---

# 📌 Project Overview

**Namaa** is a backend agricultural platform designed to modernize and digitize the agricultural ecosystem by connecting farmers, traders, investors, agricultural experts, and administrators in one unified system.

In traditional systems, each stakeholder operates in isolation. Farmers struggle to access markets, traders lack direct sourcing transparency, and experts are disconnected from real-time agricultural needs. Namaa was built to solve this fragmentation by creating a structured, scalable backend system that simulates a real-world agricultural marketplace.

---

## 🌱 The Idea Behind the Project

This project started as a graduation-level engineering initiative aimed at solving real-world inefficiencies in agriculture.

Instead of building a simple CRUD application, the goal was to design a **production-style backend system** that reflects real software engineering practices used in modern companies.

The focus was not only on implementing features, but on building a **clean, maintainable, and scalable architecture** that could support future expansion.

---

## 🏗️ How the System Was Built

The system was designed using modern backend engineering principles:

### 🧱 Clean Architecture
The project is divided into clear layers:
- **API Layer** → Handles HTTP requests, controllers, and middleware
- **Application Layer** → Contains business logic, CQRS handlers, validation
- **Domain Layer** → Core business entities and rules
- **Infrastructure Layer** → Database, external APIs, and integrations

This ensures full separation of concerns and high maintainability.

---

### ⚡ CQRS + MediatR Pattern
The system follows CQRS principles:
- Commands handle write operations
- Queries handle read operations
- MediatR manages request pipelines

This improves scalability and keeps business logic organized.

---

### 🧠 Engineering Practices
The project includes production-level backend concepts such as:
- Centralized error handling system
- FluentValidation for request validation
- JWT authentication & authorization
- Tag-based caching strategy for performance optimization
- Structured logging using Serilog

---

### 🔌 External Integrations
To simulate a real production system, multiple external services are integrated:

- 🤖 OpenAI API → AI-based agricultural consultation
- 🌦️ OpenWeatherMap API → Weather and environmental insights
- ☁️ Cloudinary → Image and media storage
- 📧 Brevo SMTP → Email notifications system

---

# 🚀 Features

- 🔐 Secure authentication & authorization (JWT)
- 👨‍🌾 Farmer management system
- 🌾 Agricultural marketplace (listings)
- ⭐ Farmer rating & review system
- 📦 Order and trading workflow system
- ☁️ Media upload system (Cloudinary)
- 🤖 AI agricultural assistant (OpenAI)
- 🌦️ Weather insights integration
- 📧 Email notification system
- ⚡ Hybrid caching with tag-based invalidation
- 🧠 Centralized error handling
- 📊 Fully structured CQRS architecture

---

# 👥 System Roles

The system is designed around multiple user roles:

| Role | Description |
|------|------------|
| 👨‍🌾 Farmer | Creates listings, manages products, receives ratings |
| 🛒 Trader | Purchases agricultural products |
| 💰 Investor | Funds agricultural opportunities |
| 🌱 Expert | Provides agricultural consultation |
| 🛠️ Admin | System moderation and management |
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

The system follows Clean Architecture principles to ensure strict separation between layers.

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

# 📋 Prerequisites

- .NET 9 SDK
- PostgreSQL installed and running
- Visual Studio / Rider / VS Code
- API keys for external services (OpenAI, Cloudinary, Brevo, Weather API)

---

# ⚙️ Configuration

Create or update:

```
Namaa.API/appsettings.Development.json
```

Example configuration:

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

### 3. Run the application
> The system automatically applies database migrations on startup.

```bash
dotnet run --project src/Namaa.API
```

---

### 4. Access Swagger API
```
https://localhost:7070/swagger
```

---

# 🧩 System Design Highlights

- ✔ Clean Architecture with strict separation of concerns
- ✔ CQRS pattern for scalable read/write operations
- ✔ Centralized error handling pipeline
- ✔ Tag-based caching strategy for performance optimization
- ✔ Modular multi-role system design
- ✔ External service integrations (AI, Weather, Email)
- ✔ Production-level backend architecture

---

# ⭐ Final Note

Namaa is a real-world backend system built using modern .NET architecture principles. It demonstrates scalable system design, clean engineering practices, and production-level backend development skills suitable for real industry applications.

# 🌾 Namaa Agricultural Backend API 🚜

[![Backend Core](https://img.shields.io/badge/.NET-9.0-purple.svg)](#)
[![Architecture](https://img.shields.io/badge/Architecture-Clean_/_CQRS-blue.svg)](#)
[![Status](https://img.shields.io/badge/Status-Active_Development-orange.svg)](#)

---

# 📌 Project Overview

**Namaa** is a backend agricultural platform designed to modernize and digitize the agricultural ecosystem by connecting farmers, traders, investors, agricultural experts, and administrators in one unified system.

In traditional systems, stakeholders operate in isolation. Farmers struggle with market access, traders lack transparency in sourcing, and experts are disconnected from real-time agricultural needs. Namaa was built to solve this fragmentation by creating a structured, scalable backend system that simulates a real-world agricultural marketplace.

> 🚧 **Important Note:** This project is currently under active development as part of a final-year Computer Engineering graduation project. The core architecture and backend foundation are stable, while some domain-specific features are still being developed.

---

## 🌱 The Idea Behind the Project

This project started as a graduation engineering initiative focused on solving real-world inefficiencies in agriculture.

Instead of building a simple CRUD system, the goal was to design a **production-style backend architecture** that reflects real-world software engineering practices used in scalable systems.

The focus is not only on features, but on building a **clean, maintainable, and extensible backend system**.

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

This improves scalability and keeps business logic clean and organized.

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
To simulate a real production system, the project integrates:

- 🤖 OpenAI API → AI agricultural assistant
- 🌦️ OpenWeatherMap API → Weather insights
- ☁️ Cloudinary → Media storage
- 📧 Brevo SMTP → Email notifications

---

# 🚀 Features

- 🔐 Authentication & Authorization (JWT)
- 👨‍🌾 Farmer management system
- 🌾 Agricultural marketplace (listings)
- ⭐ Farmer rating & review system
- 📦 Order & trading workflow system
- ☁️ Media upload system (Cloudinary)
- 🤖 AI agricultural consultation
- 🌦️ Weather integration
- 📧 Email notification system
- ⚡ Hybrid caching with tag-based invalidation
- 🧠 Centralized error handling
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

The system follows Clean Architecture principles for strict separation of concerns.

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

Before running the application, ensure required settings are configured in `appsettings.Development.json`.

You must provide:

- Database connection string (PostgreSQL)
- JWT secret key
- External API keys (if used)

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
  }
}
```

---

## ⚠️ Important Note

- 🚧 This project is still under active development
- Core backend architecture is stable and implemented
- Some domain features are still in progress
- Do NOT commit real secrets or API keys

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
> The system automatically applies database migrations on startup.

```bash
dotnet run --project src/Namaa.API
```

---

### 4. Open Swagger
```
https://localhost:7070/swagger
```

---

# 🧩 System Design Highlights

- ✔ Clean Architecture with strict separation of layers
- ✔ CQRS pattern for scalable request handling
- ✔ Centralized error handling pipeline
- ✔ Tag-based caching strategy
- ✔ Modular multi-role system design
- ✔ External integrations (AI, Weather, Email)
- ✔ Production-style backend architecture

---

# ⭐ Final Note

Namaa is a real-world backend system built using modern .NET architecture principles. It demonstrates scalable system design, clean engineering practices, and production-ready backend development suitable for real industry applications, while still being actively developed as a graduation project.

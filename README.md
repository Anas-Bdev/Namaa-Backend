# 🌾 Namaa — Agricultural Backend API

![Architecture](https://img.shields.io/badge/Architecture-Clean_/_CQRS-blue?style=flat-square) ![Status](https://img.shields.io/badge/Status-Completed-brightgreen?style=flat-square) ![.NET 9](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat-square&logo=dotnet&logoColor=white) ![C#](https://img.shields.io/badge/C%23-Ready-239120?style=flat-square&logo=c-sharp&logoColor=white) ![PostgreSQL](https://img.shields.io/badge/PostgreSQL-Passed-4169E1?style=flat-square&logo=postgresql&logoColor=white) ![OpenAI](https://img.shields.io/badge/OpenAI-Integrated-412991?style=flat-square&logo=openai&logoColor=white)

---

## 📌 Overview

**Namaa** is a backend platform that digitizes and connects the agricultural ecosystem — linking farmers, traders, investors, agricultural experts, and administrators through a single, scalable API.

The platform supports land registration, crop cycle management, product listings, marketplace trading, expert consultations, investment opportunities, weather insights, and an AI-powered assistant — all built on production-grade engineering practices rather than simple CRUD.

---

## 🏗️ Architecture

Namaa follows **Clean Architecture** with four layers:

| Layer | Responsibilities |
|---|---|
| `Namaa.API` | Controllers, authentication, exception handling, Swagger |
| `Namaa.Application` | CQRS commands/queries, MediatR handlers, DTOs, validation, pipeline behaviors |
| `Namaa.Domain` | Entities, value objects, domain rules, enumerations, typed errors |
| `Namaa.Infrastructure` | EF Core, PostgreSQL, JWT, Cloudinary, OpenAI, OpenWeatherMap, email, caching |

### Request Flow

```
Client → API Controllers → MediatR Pipeline → CQRS Handler → Domain Logic → Infrastructure → DB / External Services
```

**Pipeline Behaviors (in order):** Unhandled Exception → Performance Monitoring → Active User Authorization → Caching → Validation

---

## ⚡ Key Patterns

- **CQRS + MediatR** — Commands mutate state; queries read data; MediatR orchestrates.
- **Result Pattern** — All operations return strongly-typed results; no exceptions for flow control.
- **Typed Error System** — Centralized, categorized errors (Validation, NotFound, Conflict, Unauthorized, Forbidden).
- **RFC 7807 Problem Details** — All errors map to standardized HTTP responses via `IExceptionHandler`.

---

## 🚀 Features

**Security & Identity**
- JWT authentication & authorization
- Email verification, OTP-based password reset
- Active-user authorization pipeline behavior

**Agricultural Management**
- Farmer profiles, land registration, crop & seeding cycle management, farm activity tracking

**Marketplace & Trading**
- Product listings, order management, farmer ratings and reviews

**Investment & Consultation**
- Agricultural investment opportunities, expert consultations, AI-powered assistant

**Integrations**
- Cloudinary (media), OpenAI (AI assistant), OpenWeatherMap (weather), Brevo SMTP (email)

---

## 👥 System Roles

| Role | Description |
|---|---|
| 👨‍🌾 Farmer | Registers lands, manages crop cycles, creates listings, receives ratings |
| 🛒 Trader | Purchases products, reviews farmers |
| 💰 Investor | Funds agricultural opportunities |
| 🌱 Expert | Provides consultations and guidance |
| 🛠️ Administrator | Manages and moderates the platform |
| 👤 Guest | Public browsing access |

---

## 🛠️ Tech Stack

| Concern | Technology |
|---|---|
| Framework | ASP.NET Core 9 |
| ORM / Database | EF Core 9 + PostgreSQL |
| Auth | JWT Bearer |
| Validation | FluentValidation |
| Mediator | MediatR (CQRS) |
| Logging | Serilog |
| Caching | ASP.NET Core HybridCache |
| Storage | Cloudinary |
| AI | OpenAI API |
| Weather | OpenWeatherMap API |
| Email | MailKit + Brevo SMTP |

---

## ⚙️ Configuration

Edit `appsettings.json` with your local values:

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
  "OpenAi": { "ApiKey": "your_api_key" },
  "WeatherApi": { "OpenWeatherMapKey": "your_api_key" },
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

> ⚠️ Never commit real secrets or API keys to version control.

---

## 🚀 Getting Started

```bash
# 1. Clone
git clone https://github.com/YOUR_USERNAME/Namaa-Backend.git
cd Namaa-Backend

# 2. Restore dependencies
dotnet restore

# 3. Run (migrations apply automatically on startup)
dotnet run --project src/Namaa.API
```

Swagger UI available at: `https://localhost:7070/swagger`

---

## 👥 Contributors

Developed as a graduation engineering project.

**Anas Haj Mohammad** — Software Engineer | [GitHub](https://github.com/Anas-Haj-Mohammad)
Architecture design, CQRS, domain modeling, caching, error handling, core implementation.

**Ala'a Abu Musa** — Software Engineer | [GitHub](https://github.com/alaaabumusa)

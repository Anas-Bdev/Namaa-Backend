
# 🌾 Namaa — Agricultural Backend Platform

![Architecture](https://img.shields.io/badge/Architecture-Clean_/_CQRS-blue?style=flat-square) ![Status](https://img.shields.io/badge/Status-Completed-brightgreen?style=flat-square) ![.NET 9](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat-square&logo=dotnet&logoColor=white) ![C#](https://img.shields.io/badge/C%23-Ready-239120?style=flat-square&logo=c-sharp&logoColor=white) ![PostgreSQL](https://img.shields.io/badge/PostgreSQL-Powered-4169E1?style=flat-square&logo=postgresql&logoColor=white) ![OpenAI](https://img.shields.io/badge/OpenAI-Integrated-412991?style=flat-square&logo=openai&logoColor=white)

---

# 📌 Overview

**Namaa** is a  backend platform that digitizes and connects the agricultural ecosystem, linking farmers, traders, investors, agricultural experts, and administrators through a unified API.

The platform supports:

- 🌱 Land Registration
- 🌾 Crop & Seeding Cycle Management
- 🛒 Marketplace Trading
- 💰 Agricultural Investment Opportunities
- 👨‍🌾 Farmer Management
- 👨‍⚕️ Expert Consultations
- 🌦️ Weather Insights
- 🤖 AI-Powered Agricultural Assistant

Rather than being a simple CRUD application, Namaa was designed using real backend engineering practices, including Clean Architecture, CQRS, centralized error handling, caching, and validation pipelines to keep the system maintainable and extensible.

---

# 📚 Table of Contents

- [📌 Overview](#-overview)
- [🎯 Project Goal](#-project-goal)
- [📊 Project Highlights](#-project-highlights)
- [🧱 Architecture](#-architecture)
- [🔄 Request Flow](#-request-flow)
- [🔁 Pipeline Behaviors](#-pipeline-behaviors)
- [⚡ Engineering Highlights](#-engineering-highlights)
- [🚀 Features](#-features)
  - [🔐 Security & Identity](#-security--identity)
  - [🌱 Agricultural Management](#-agricultural-management)
  - [🛒 Marketplace & Trading](#-marketplace--trading)
  - [💰 Investment Opportunities](#-investment-opportunities)
  - [🩺 Expert Consultation](#-expert-consultation)
  - [🤖 AI Assistant](#-ai-assistant)
  - [🔌 External Integrations](#-external-integrations)
- [👥 System Roles](#-system-roles)
- [💽 Database Design](#-database-design)
- [🧰 Tech Stack](#-tech-stack)
- [🔧 Configuration](#-configuration)
- [🚀 Getting Started](#-getting-started)
- [📄 API Documentation](#-api-documentation)
- [💻 My Contributions](#-my-contributions)
- [👥 Contributors](#-contributors)
- [📄 Copyright](#-copyright)

---

# 🎯 Project Goal

Namaa was developed as a graduation engineering project to explore how modern software engineering practices can be applied to build a scalable agricultural management platform.

The system brings together multiple stakeholders — including farmers, traders, investors, and agricultural experts — through a unified backend API that supports agricultural operations, marketplace activities, consultations, and investment opportunities.

Beyond the core business features, this project gave me the opportunity to design and build a real backend from the ground up — using Clean Architecture, CQRS, validation pipelines, centralized error handling, caching, and external service integrations.

---

# 📊 Project Highlights

- 🌐 ASP.NET Core 9
- 💻 C#
- 🐘 PostgreSQL
- 🗃️ Entity Framework Core 9
- 🏛️ Clean Architecture
- 🔀 CQRS + MediatR
- 🔐 JWT Authentication + Refresh Tokens
- ✅ FluentValidation
- 📝 Serilog Logging
- 💾 HybridCache
- 🤖 OpenAI Integration
- 🌦️ OpenWeatherMap Integration
- ☁️ Cloudinary Integration
- 📧 MailKit + Brevo SMTP
- 📄 RFC 7807 Problem Details
- 🌍 Global Exception Handling
- ⚡ Performance Monitoring
- 🚨 Typed Error System

---

# 🧱 Architecture

Namaa follows **Clean Architecture** with a clear separation of concerns.

| Layer | Responsibilities |
|---|---|
| **Namaa.API** | Controllers, authentication, authorization, Swagger, exception handling |
| **Namaa.Application** | CQRS commands & queries, MediatR handlers, DTOs, validation, pipeline behaviors |
| **Namaa.Domain** | Entities, domain rules, enumerations, typed errors |
| **Namaa.Infrastructure** | EF Core, PostgreSQL, JWT, Cloudinary, OpenAI, weather API, email services, caching |

---

## 🔄 Request Flow

```text
Client
  ↓
API Controllers
  ↓
MediatR Pipeline
  ↓
Command / Query Handler
  ↓
Domain Logic
  ↓
Infrastructure
  ↓
Database / External Services
```

---

## 🔁 Pipeline Behaviors

```text
Unhandled Exception
  ↓
Performance Monitoring
  ↓
Active User Authorization
  ↓
Caching
  ↓
Validation
```

---

# ⚡ Engineering Highlights

### 🔀 CQRS + MediatR
Commands mutate state while queries retrieve data. MediatR orchestrates requests and cross-cutting concerns through pipeline behaviors.

### 🔐 JWT Authentication & Refresh Token Management
Implemented secure authentication using JWT access tokens with refresh token support, allowing users to maintain long-lived sessions without repeatedly providing credentials. Refresh tokens are stored and managed to support token renewal and session revocation.

### 📦 Result Pattern
Application operations return strongly typed results instead of relying on exceptions for normal control flow.

### 🚨 Typed Error System
Centralized error definitions categorized into:
- Validation
- Not Found
- Conflict
- Unauthorized
- Forbidden

### 🌍 Global Exception Handling
Centralized exception handling is implemented using ASP.NET Core's `IExceptionHandler`. All failures are transformed into RFC 7807 Problem Details responses for consistent API behavior.

### ✅ Validation Pipeline
FluentValidation is integrated into the MediatR pipeline, ensuring requests are validated before reaching handlers.

### ⚡ Performance Monitoring
A custom MediatR behavior tracks request execution times and logs slow-running operations.

### 💾 Caching
ASP.NET Core HybridCache reduces database load and improves response times for frequently requested data.

### 📝 Structured Logging
Serilog provides structured logging and diagnostics across the application.

---

# 🚀 Features

## 🔐 Security & Identity

* 📝 Register and log in with JWT-secured authentication
* 🔄 Refresh token mechanism for maintaining secure long-lived user sessions
* 🚪 Refresh token revocation during logout
* ✉️ Verify email address after registration
* 🔢 Reset password via OTP
* 🧑‍⚖️ Access is controlled by role-based permissions

---

## 🌱 Agricultural Management

* 👨‍🌾 Create and manage farmer profiles
* 🗺️ Register agricultural lands
* 🌾 Create and manage crop cycles
* 🌱 Track seeding cycles
* 📈 Monitor farm activity over time

---

## 🛒 Marketplace & Trading

* 🏷️ Publish and manage product listings
* 🔍 Browse the marketplace
* 🧾 Place and manage orders
* ⭐ Rate and review farmers

---

## 💰 Investment Opportunities

* 📢 Create agricultural investment opportunities
* 🤝 Invest in agricultural projects as an investor
* 🗂️ Manage and track funding requests

---

## 🩺 Expert Consultation

* 📅 Farmers submit consultation requests to agricultural experts
* ✅ Experts review and respond to consultation requests
* 💬 Supports communication between farmers and experts during consultations

---

## 🤖 AI Assistant

* 🌾 Get AI-powered crop recommendations via OpenAI
* 💬 Ask agricultural consultation questions and get AI-generated answers

---

## 🔌 External Integrations

- Cloudinary — media storage
- OpenAI API — AI assistant
- OpenWeatherMap API — weather insights
- MailKit + Brevo SMTP — transactional email

---

# 👥 System Roles

| Role | Description |
|---|---|
| 👨‍🌾 Farmer | Registers lands, manages crop cycles, creates listings, receives ratings |
| 🛒 Trader | Purchases products and reviews farmers |
| 💰 Investor | Funds agricultural opportunities |
| 🌱 Expert | Provides consultations and guidance |
| 🛠️ Administrator | Moderates and manages the platform |
| 👤 Guest | Public browsing access |

---

# 💽 Database Design

The following Entity Relationship Diagram (ERD) represents the database schema used by Namaa. It illustrates the relationships between users, agricultural lands, crop cycles, marketplace entities, consultations, investments, and other supporting domain objects.

![Database ERD](Database-ERD.jpg)
*Entity Relationship Diagram (ERD) of the Namaa platform database.*

---

# 🧰 Tech Stack

| Concern | Technology |
|---|---|
| Framework | ASP.NET Core 9 |
| Language | C# |
| Database | PostgreSQL |
| ORM | Entity Framework Core 9 |
| Authentication | JWT Bearer |
| Validation | FluentValidation |
| CQRS | MediatR |
| Logging | Serilog |
| Caching | ASP.NET Core HybridCache |
| Storage | Cloudinary |
| AI | OpenAI API |
| Weather | OpenWeatherMap API |
| Email | MailKit + Brevo SMTP |

---

# 🔧 Configuration

Configure `appsettings.json` with your local values:

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

# 🚀 Getting Started

### 1️⃣ Clone the repository
```bash
git clone https://github.com/Anas-Bdev/Namaa-Backend.git
cd Namaa-Backend
```

### 2️⃣ Restore dependencies
```bash
dotnet restore
```

### 3️⃣ Run the application
```bash
dotnet run --project src/Namaa.API
```

---

# 📄 API Documentation

Swagger UI is available at:

```text
https://localhost:7070/swagger
```

---
## 📄 API Endpoints

Namaa provides a RESTful API organized around different business domains.  
The following endpoints demonstrate the main workflows supported by the platform.

Full API documentation is available through Swagger/OpenAPI.

---

### 🔐 Identity & Authentication

| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/identity/register` | Register a new user account |
| POST | `/api/identity/register/expert` | Register an agricultural expert with CV upload |
| POST | `/api/identity/login` | Authenticate user and generate JWT access and refresh tokens |
| POST | `/api/identity/refresh-token` | Refresh expired access tokens |
| GET | `/api/identity/confirm-email` | Confirm user email address |
| POST | `/api/identity/forgot-password` | Request password reset |
| POST | `/api/identity/reset-password` | Reset user password |
| POST | `/api/identity/logout` | Logout and revoke refresh token |

---

### 🌱 Agricultural Land Management

| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/lands` | Register a new agricultural land |
| GET | `/api/lands` | Get farmer's registered lands |
| GET | `/api/lands/{id}` | Get land details |
| PUT | `/api/lands/{id}` | Update land information |
| DELETE | `/api/lands/{id}` | Delete agricultural land |
| GET | `/api/lands/{landId}/crops` | Get suitable crops for a land |

---

### 💰 Investment Management

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/investments/projects/funding` | Browse available investment projects |
| GET | `/api/investments/projects/{id}` | Get investment project details |
| POST | `/api/investments/projects` | Create investment opportunity |
| PUT | `/api/investments/projects/{id}/start-progress` | Start investment project |
| PUT | `/api/investments/projects/{id}/complete` | Complete investment project |
| POST | `/api/investments/projects/{projectId}/contributions` | Create investor contribution |
| PUT | `/api/investments/contributions/{id}/approve` | Approve investor contribution |
| PUT | `/api/investments/contributions/{id}/reject` | Reject investor contribution |

---

### 🛒 Marketplace & Trading

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/marketplace/listings` | Browse marketplace listings |
| GET | `/api/marketplace/listings/{listingId}` | Get product listing details |
| POST | `/api/marketplace/listings` | Create product listing |
| PUT | `/api/marketplace/listings/{listingId}` | Update product listing |
| PUT | `/api/marketplace/listings/{listingId}/archive` | Archive product listing |
| POST | `/api/marketplace/orders` | Create product order |
| GET | `/api/marketplace/orders/{orderId}` | Get order details |
| PUT | `/api/marketplace/orders/{orderId}/confirm` | Confirm order |
| PUT | `/api/marketplace/orders/{orderId}/pay` | Pay for order |
| PUT | `/api/marketplace/orders/{orderId}/ship` | Ship order |
| PUT | `/api/marketplace/orders/{orderId}/deliver` | Mark order as delivered |
| POST | `/api/marketplace/orders/{orderId}/rating` | Rate farmer after order completion |

---

### 🩺 Expert Consultation System

| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/consultations` | Request agricultural consultation |
| GET | `/api/consultations/my-consultations` | Get farmer consultations |
| GET | `/api/consultations/available` | Get available consultations for experts |
| GET | `/api/consultations/assigned` | Get expert assigned consultations |
| GET | `/api/consultations/{id}` | Get consultation details |
| POST | `/api/consultations/{id}/messages` | Add consultation message |
| PUT | `/api/consultations/{id}/assign` | Assign expert to consultation |
| PUT | `/api/consultations/{id}/close` | Close consultation |
| POST | `/api/consultations/ai-preview` | Get AI-powered agricultural advice |

---

# 💻 My Contributions

![Primary Developer](https://img.shields.io/badge/Role-Primary_Backend_Developer-blueviolet?style=flat-square)

I worked on this backend as part of a two-person backend team. I was responsible for designing the overall backend architecture and implementing the majority of the core functionality in this repository. My main contributions include:

### 🏛️ Architecture & Design
- 🧱 Designed and implemented the full Clean Architecture structure (API, Application, Domain, Infrastructure)
- 🔀 Applied CQRS using MediatR across the entire application layer
- 📐 Designed domain models, entities, and business rules

### 🚨 Reliability & Error Handling
- 📦 Developed the Result Pattern and Typed Error System from scratch
- 🌍 Built centralized global exception handling using RFC 7807 Problem Details
- ✅ Set up the FluentValidation pipeline behavior

### ⚡ Performance & Infrastructure
- 💾 Implemented caching (HybridCache) and performance monitoring pipeline behaviors
- 🗄️ Configured PostgreSQL and Entity Framework Core
- 🔌 Integrated all external services: OpenAI, Cloudinary, OpenWeatherMap, MailKit/Brevo

### 🚀 Core Functionality
- ⚙️ Implemented core application functionality end-to-end, including authentication workflows (JWT access tokens, refresh tokens, email verification, password reset), agricultural management, marketplace, investments, consultations, and AI assistant features.
- 📝 Owned structured logging setup with Serilog

---

# 👥 Contributors

This backend is part of a larger graduation engineering project, built by the two of us below.

### 👨‍💻 Anas Haj Mohammad — Lead Backend Engineer
**Contributions:** Architecture design, CQRS implementation, domain modeling, error handling, caching, and core backend implementation.
GitHub: [Anas-Bdev](https://github.com/Anas-Bdev)

### 👩‍💻 Alaa Abu Musa — Backend Engineer
GitHub: [alaaabumusa](https://github.com/alaaabumusa)

---

# 📄 Copyright

© 2026 Namaa Project Team. All Rights Reserved.

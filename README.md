# 🌾 Namaa Agricultural Backend API 🚜

[![Backend Core](https://img.shields.io/badge/.NET-8.0-purple.svg)](#)
[![Architecture](https://img.shields.io/badge/Architecture-Clean_/_CQRS-blue.svg)](#)
[![Status](https://img.shields.io/badge/Status-Active_Development-orange.svg)](#)

Namaa is an enterprise-grade, integrated agricultural management platform designed to digitize and scale the agricultural ecosystem. The system provides a robust, decoupled, and secure backend API that bridges communication, tracking, and transactional pipelines between **Farmers, Agricultural Experts, Traders, and Investors**. 

> 🚧 **Note to Engineering Reviewers:** This project is actively under development as part of my final-year computer engineering graduation requirements. While certain domain-specific features are being actively built out, the entire structural blueprint, security filters, data persistence layers, and advanced cache invalidation pipelines are 100% established, functional, and ready for code review.

---

## 📌 Table of Contents
* [Overview](#-overview)
* [Core Engineering Features](#-core-engineering-features)
* [Tech Stack](#-tech-stack)
* [Configuration & Local Environment (`appsettings.json`)](#-configuration--local-environment-appsettingsjson)
* [🗂️ Project Architecture & Folder Structure](#%EF%B8%8F-project-architecture--folder-structure)
* [📚 API Documentation](#-api-documentation)
  * [Authentications](#authentications)
  * [User & Profile Management](#user--profile-management)
  * [Farmer Operations](#farmer-operations)
  * [Expert Consultations](#expert-consultations)
  * [Marketplace & Trade](#marketplace--trade)

---

## 📝 Overview
The primary objective of Namaa is to solve systemic communication and tracking challenges within the agricultural sector. By structuring the backend using **Clean Architecture** and **CQRS**, the system ensures that complex domain logics (like soil analysis verification, matching algorithms, or bulk supply pricing) remain fully isolated, clean, and completely testable without relying on side effects from web frameworks or specific database systems.

---

## ⚡ Core Engineering Features

*   **Clean Architecture Blueprint:** Complete decoupling into Domain, Application, Infrastructure, and Web API projects to protect core domain logic from external changes.
*   **CQRS Orchestration:** Utilizing **MediatR** to split system operations into specialized Read Queries and Write Commands, eliminating fat services and bloated controllers.
*   **Defensive Pipeline Filters:** Implementation of global **FluentValidation** pipeline behaviors inside MediatR to instantly capture and short-circuit malformed API requests before they penetrate the business layers.
*   **Active Cache Invalidation:** Optimized request management utilizing hybrid caching mechanics coupled with proactive, tag-based eviction strategies to protect persistence layers while delivering sub-millisecond responses.
*   **Global Error Handling Middleware:** Centralized middleware filtering that intercepts exceptions across all architectural layers, sanitizes logs, and returns standard RFC-compliant error responses to clients.

---

## 🛠️ Tech Stack
*   **Framework:** ASP.NET Core Web API (.NET 8)
*   **Data Persistence:** Entity Framework Core (Code-First Approach)
*   **Databases:** SQL Server / PostgreSQL compatibility
*   **Security & Identity:** JSON Web Tokens (JWT) & Role-Based Authorization Filter Engines
*   **Libraries:** MediatR, FluentValidation, AutoMapper, MailKit

---

## ⚙️ Configuration & Local Environment (`appsettings.json`)

To run this project locally, copy the foundational keys listed in the template below. 

⚠️ **Security Enforcement:** Production secret certificates and live connection strings are completely decoupled from source control using environment variable overrides. The `appsettings.json` file contains only safe placeholder mocks.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_LOCAL_SERVER;Database=NamaaDB;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "JwtSettings": {
    "Secret": "YOUR_SUPER_SECRET_UNSHAKEABLE_DEVELOPMENT_KEY_MIN_256_BITS",
    "Issuer": "NamaaIdentityServer",
    "Audience": "NamaaGateway",
    "ExpiryMinutes": 60
  },
  "MailSettings": {
    "Host": "smtp.mailtrap.io",
    "Port": 587,
    "UserName": "MOCK_DEVELOPMENT_USERNAME",
    "Password": "MOCK_DEVELOPMENT_PASSWORD"
  }
}

Feel free to explore the core architecture inside the `src/` directory:
*   **Namaa.Domain:** Contains core entities, value objects, domain exceptions, and specifications.
*   **Namaa.Application:** Houses the CQRS commands, queries, handlers, mapping profiles, and validation behaviors.
*   **Namaa.Infrastructure:** Handles external concerns like database contexts, migrations, security implementations, and logging.
*   **Namaa.API:** The presentation layer containing RESTful controllers, global error-handling middlewares, and configuration setups.

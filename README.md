# 🌾 Namaa Agricultural Backend API 🚜

[![Backend Core](https://img.shields.io/badge/.NET-9.0-purple.svg)](#)
[![Architecture](https://img.shields.io/badge/Architecture-Clean_/_CQRS-blue.svg)](#)
[![Status](https://img.shields.io/badge/Status-Active_Development-orange.svg)](#)

Namaa is an enterprise-grade, integrated agricultural management platform designed to digitize and scale the agricultural ecosystem. The system provides a robust, decoupled, and secure backend API that bridges communication, tracking, and transactional pipelines between **Farmers, Agricultural Experts, Traders, and Investors**. 

> 🚧 **Note to Engineering Reviewers:** This project is actively under development as part of my final-year computer engineering graduation requirements at Palestine Technical University - Kadoorie (Expected Graduation: June 2026). While certain domain-specific features are being actively built out, the entire structural blueprint, security filters, data persistence layers, and advanced cache invalidation pipelines are 100% established, functional, and ready for code review.

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

## 🎯 Project Thesis: Problem Statement & Unified Solution

### 🛑 The Problem: A Fragmented Agricultural Ecosystem
The agricultural sector consistently suffers from critical operational inefficiencies caused by highly fragmented communication pipelines and a lack of centralized coordination. Stakeholders experience severe bottlenecks across three main dimensions:
1.  **Isolation of Agronomic Expertise:** Farmers frequently lack immediate, reliable access to scientific experts to diagnose soil, crop, or pest problems, leading to preventable yield losses.
2.  **Market Exploitation & Opaque Supply Chains:** Without a transparent, direct marketplace platform, intermediary traders often exploit pricing data asymmetric gaps, squeezing the profit margins of local farmers.
3.  **Fragmented Stakeholder Coordination:** Investors, suppliers, and producers operate in data silos, making large-scale tracking, agricultural monitoring, and resource optimization incredibly difficult to manage cohesively.

Building a basic, standard monolithic application to handle these complex, intersecting business domains inevitably results in "spaghetti code"—where security rules, data access, and core business models are tightly coupled, making the application fragile and impossible to scale.

---

### 💡 The Solution: Namaa's Architecture-First Approach
**Namaa** solves these ecosystem inefficiencies by serving as a secure, unified backend orchestrator that bridges the operational workflows of Farmers, Experts, Traders, and Investors. 

To prevent the platform from becoming an unmaintainable monolith, the backend is strictly engineered around **Clean Architecture (Onion Pattern)** and **CQRS (Command Query Responsibility Segregation)** using **.NET 9**. 

By completely isolating the core business rules from external infrastructure dependencies (like databases or web frameworks), the system achieves:
*   **Decoupled Domain Invariants:** Core agricultural business rules (such as matching algorithms or data access controls) are independent of infrastructure changes.
*   **High Horizontal Scalability:** High-traffic read paths (like browsing marketplace products) are completely separated from heavy write operations (like processing structural land records or system changes), avoiding database gridlocks.
*   **Enterprise Testability:** Because the application layer interacts solely with abstractions, unit testing complex business use cases can be performed perfectly without mocking live database states.

---

### ⚙️ What the System Executes (Core Capabilities)

The platform translates this high-level architecture into precise, production-ready backend capabilities:

*   **Stakeholder Coordination Engine:** Provides distinct data models, claims, and role-based access security engines tailored dynamically for Farmers, Experts, Traders, and Investors.
*   **Consultation & Resource Monitoring Pipelines:** Manages secure tracking systems for land records, field histories, and crop monitoring metrics alongside automated consultation request routers.
*   **High-Performance Marketplace Matrix:** Powers optimized, paginated trade supply directories protected by tag-based invalidation mechanics to maximize request-response speeds.
*   **Defensive Application Gateways:** Processes all incoming data mutations through localized validation checkpoints, guaranteeing that malformed data payloads are completely intercepted and blocked before hitting the persistence database.

---

## ⚡ Core Engineering Features

*   **Clean Architecture Blueprint:** Complete decoupling into Domain, Application, Infrastructure, and Web API projects to protect core domain logic from external changes.
*   **CQRS Orchestration:** Utilizing **MediatR** to split system operations into specialized Read Queries and Write Commands, eliminating fat services and bloated controllers.
*   **Defensive Pipeline Filters:** Implementation of global **FluentValidation** pipeline behaviors inside MediatR to instantly capture and short-circuit malformed API requests before they penetrate the business layers.
*   **Modern .NET 9 HybridCache:** Optimized request management utilizing the new native `.NET 9 HybridCache` engine coupled with proactive, tag-based eviction strategies to deliver sub-millisecond responses while protecting persistence layers.
*   **Global Error Handling Middleware:** Centralized middleware filtering that intercepts exceptions across all architectural layers, sanitizes logs, and returns standard RFC-compliant error responses to clients.

---

## 🛠️ Tech Stack
*   **Framework:** ASP.NET Core Web API (.NET 9)
*   **Data Persistence:** Entity Framework Core 9 (Code-First Approach)
*   **Databases:** SQL Server / PostgreSQL compatibility
*   **Caching Layer:** .NET 9 Microsoft.Extensions.Caching.Hybrid
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

# 🚧 Namaa Agricultural Platform (Backend API)

### 🎓 Graduation Project | Palestine Technical University - Kadoorie
**Status:** Active Development (Expected Graduation: June 2026)

Namaa is an integrated, enterprise-level agricultural platform designed to streamline communication, resource monitoring, and stakeholder coordination by seamlessly connecting farmers, agricultural experts, traders, and investors.

> 💡 **Note to Reviewers:** This repository contains the core Web API backend for Namaa. It is intentionally built using enterprise design patterns rather than a simple monolith. While business domain features are being actively expanded ahead of graduation, the foundational infrastructure, architectural pipelines, security measures, and database layers are fully implemented, functional, and open for review.

---

## 🏗️ Architectural Blueprint

The backend is built using a highly decoupled architecture to ensure strict separation of concerns, high testability, and smooth scalability:

*   **Clean Architecture (Onion Architecture):** Divided strictly into Domain, Application, Infrastructure, and Web API layers to decouple core business logic from external frameworks.
*   **CQRS Pattern (Command Query Responsibility Segregation):** Implemented using **MediatR** to split read operations from write mutations, reducing side effects and optimizing request paths.
*   **Defensive Design & Validation Pipelines:** Utilizing **FluentValidation** via MediatR behaviors to cleanly intercept requests and handle validation errors globally before they ever hit the domain.

---

## 🛠️ Tech Stack & Technologies

*   **Framework:** .NET Core / ASP.NET Core Web API
*   **Data Access:** Entity Framework Core (Code-First approach)
*   **Database:** SQL Server / PostgreSQL
*   **Security:** JWT Token Authentication & Role-Based Authorization
*   **Libraries & Tools:** MediatR, FluentValidation

---

## 📂 Codebase Tour

Feel free to explore the core architecture inside the `src/` directory:
*   **Namaa.Domain:** Contains core entities, value objects, domain exceptions, and specifications.
*   **Namaa.Application:** Houses the CQRS commands, queries, handlers, mapping profiles, and validation behaviors.
*   **Namaa.Infrastructure:** Handles external concerns like database contexts, migrations, security implementations, and logging.
*   **Namaa.API:** The presentation layer containing RESTful controllers, global error-handling middlewares, and configuration setups.

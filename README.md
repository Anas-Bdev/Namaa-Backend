# 🌾 Namaa Agricultural Backend API 🚜

[![Backend Core](https://img.shields.io/badge/.NET-9.0-purple.svg)](#)
[![Architecture](https://img.shields.io/badge/Architecture-Clean_/_CQRS-blue.svg)](#)
[![Status](https://img.shields.io/badge/Status-Active_Development-orange.svg)](#)

Namaa is an enterprise-grade, integrated agricultural management platform designed to digitize and scale the agricultural ecosystem. The system provides a robust, decoupled, and secure backend API that bridges communication, tracking, and transactional pipelines across six core system roles: **Farmers, Investors, Agricultural Experts, Traders, Administrators, and Guests**. 

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

## 📝 Project Overview & Thesis

### 🛑 The Problem
The Palestinian agricultural sector is a vital economic and cultural pillar, yet its contribution to national GDP has steeply declined from roughly 13.3% in 1994 to 5.75% in 2022. This decline is driven by critical structural inefficiencies:
* **Data Silos:** Farmers, agronomic experts, wholesale traders, and investors operate in isolated communication channels.
* **Resource Optimization Bottlenecks:** A distinct lack of data-driven tracking tools for crop life cycles, soil metrics, and optimal crop scheduling.
* **Market Access Barriers:** Physical movement restrictions and predatory intermediary markups heavily penalize local farm profit margins.

### 💡 The Solution: Namaa
**Namaa** is an integrated web platform built to modernize agricultural management in Palestine. Engineered using an architecture-first approach with **.NET 9**, the backend serves as a secure, decoupled coordinator across the entire agricultural lifecycle.

By basing the platform on **Clean Architecture** and **CQRS**, core business rules remain strictly isolated from volatile infrastructure concerns. High-traffic read operations are fully decoupled from mutation commands, guaranteeing system stability and enterprise-grade testability.

---

## 🏗️ Architectural Lifecycle & Request Flow

To maintain absolute separation of concerns, data transfers strictly respect layer boundaries. Every API request passes through a defensive pipeline before executing business logic:

```text
[ Client Request ]
       │
       ▼
 [ Namaa.API ] ──────────► Global Exception Middleware (RFC Error Sanitizer)
       │
       ▼
 [ Namaa.Application ] ──► MediatR Pipeline Interceptors
       │                      │
       │                      ├──► FluentValidation Behavior (Defensive Short-Circuit)
       │                      └──► Performance Caching Engine
       ▼
 [ Handlers (CQRS) ] ────► Domain Entities / Aggregates (Namaa.Domain)
       │
       ▼
 [ Namaa.Infrastructure ]► EF Core 9 ──► Microsoft SQL Server & Cache Pools

# Restaurants API

Welcome to the **Restaurants API**! This project provides an ASP.NET Core 8 Web API designed using clean architecture principles and leveraging various Azure services for scalability, security, and performance. This API offers a reliable backend for managing restaurant data, including CRUD operations for restaurants, menu items, and orders.

## Table of Contents
- [Project Overview](#project-overview)
- [Technologies Used](#technologies-used)
- [Architecture](#architecture)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [API Endpoints](#api-endpoints)
- [Azure Services](#azure-services)
- [Contributing](#contributing)
- [License](#license)

## Project Overview
The Restaurants API provides RESTful services to manage restaurant information and customer orders. It serves as a backend solution for restaurant applications, with endpoints for adding, updating, retrieving, and deleting data related to restaurants, menus, and orders.

## Technologies Used
- **ASP.NET Core 8 Web API** for backend development
- **Clean Architecture** to separate concerns and maintain flexibility
- **Azure Services** for cloud-based scalability and secure data handling
  - Azure SQL Database
  - Azure Blob Storage for image and asset storage
  - Azure Key Vault for secure management of sensitive information
  - Azure App Service for API hosting
  - Azure Application Insights for monitoring and diagnostics

## Architecture
This project follows the Clean Architecture principles, which separate the core logic, infrastructure, and API into distinct layers:
1. **Core Layer**: Contains business logic, domain models, and application interfaces.
2. **Application Layer**: Implements use cases, data validation, and domain logic.
3. **Infrastructure Layer**: Provides implementations for data access, Azure service integrations, and external dependencies.
4. **Presentation Layer**: ASP.NET Core 8 Web API project serving as the entry point.

This structure ensures the project is modular, testable, and easy to maintain and scale.

## Getting Started
To set up the project locally:

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/arshadahamed/Restaurants-API.git
   cd Restaurants-API

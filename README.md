# ShopCx Cart Service

A .NET 6-based shopping cart service for the ShopCx demo application with intentionally vulnerable endpoints. This service handles cart management, item operations, and cart persistence using Redis for session storage.

## Security Note

⚠️ **This is an intentionally vulnerable application for security testing purposes. Do not deploy in production or sensitive environments.**

## Overview

The Cart Service is a .NET 6 Web API that provides shopping cart functionality for the ShopCx e-commerce platform. It handles cart creation, item management, and cart persistence using Redis as the backend storage with intentionally vulnerable deserialization patterns.

## Key Features

- **Cart Management**: Create, update, and retrieve user shopping carts
- **Item Operations**: Add, remove, and modify items in shopping carts
- **Redis Storage**: Fast, in-memory cart data persistence
- **RESTful API**: Standard HTTP endpoints for cart operations
- **Admin Functions**: Administrative cart clearing capabilities
- **JSON Deserialization**: Dynamic cart data handling with Newtonsoft.Json
- **Session Management**: User-specific cart isolation
- **Cart Clearing**: Bulk cart deletion functionality

## Technology Stack

- **.NET 6**: Modern web framework
- **ASP.NET Core Web API**: RESTful service framework
- **Redis**: In-memory data store for cart persistence
- **StackExchange.Redis**: Redis client library
- **Newtonsoft.Json**: JSON serialization and deserialization
- **Swashbuckle**: OpenAPI/Swagger documentation
- **Entity Framework Core**: Data access framework (included)
- **JWT Bearer Authentication**: Token-based authentication (included)
- **SQL Server**: Database provider (included)

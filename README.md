# ShopCx Cart Service

This is the shopping cart service for the intentionally vulnerable ShopCx demo application. It manages user shopping carts, cart items, and cart operations using Redis for session storage.

## Overview

The Cart Service is a .NET 6 Web API that provides shopping cart functionality for the ShopCx e-commerce platform. It handles cart creation, item management, and cart persistence using Redis as the backend storage.

## Key Features

- **Cart Management**: Create, update, and retrieve user shopping carts
- **Item Operations**: Add, remove, and modify items in shopping carts
- **Redis Storage**: Fast, in-memory cart data persistence
- **RESTful API**: Standard HTTP endpoints for cart operations
- **Admin Functions**: Administrative cart clearing capabilities

## Technology Stack

- **.NET 6**: Modern web framework
- **ASP.NET Core Web API**: RESTful service framework
- **Redis**: In-memory data store for cart persistence
- **Newtonsoft.Json**: JSON serialization
- **StackExchange.Redis**: Redis client library
- **Swashbuckle**: OpenAPI/Swagger documentation

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Cart/{userId}` | Retrieve user's cart |
| PUT | `/api/Cart/{userId}` | Update entire cart |
| POST | `/api/Cart/{userId}/items` | Add item to cart |
| DELETE | `/api/admin/clear-all` | Clear all carts (admin) |

## Dependencies

- **Redis Server**: Required for cart data storage
  - Default connection: `localhost:6379`
  - Password: `redis123`

## Build & Run

### Prerequisites
- .NET 6 SDK
- Redis server running locally

### Local Development
```bash
# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run the service
dotnet run
```

The service will start on `https://localhost:5001` (HTTPS) or `http://localhost:5000` (HTTP).

### Docker
```bash
# Build Docker image
docker build -t shopcx-cart-service .

# Run container
docker run -p 80:80 shopcx-cart-service
```

## Configuration

### Environment Variables
- `REDIS_CONNECTION`: Redis connection string
- `ASPNETCORE_ENVIRONMENT`: Application environment

### Default Configuration
```json
{
  "ConnectionStrings": {
    "Redis": "localhost:6379,password=redis123"
  }
}
```

## Health Check

The service includes a health check endpoint:
- **Endpoint**: `/health`
- **Returns**: Service status and dependency health

## API Documentation

Swagger UI is available at `/swagger` when running in development mode.

## Security Note

⚠️ **This is an intentionally vulnerable application for security testing purposes. Do not deploy in production environments.**

## Recommended Checkmarx One Configuration
- Criticality: 4
- Cloud Insights: Yes
- Internet-facing: Yes

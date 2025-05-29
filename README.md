# ShopCx Cart Service

This is the shopping cart service for the ShopCx demo application. It manages user shopping carts, cart items, and cart operations.

## Purpose
This service is designed for application security testing and education purposes. It intentionally contains various security vulnerabilities to demonstrate common issues in web applications.

## Architecture
- Written in C# (.NET)
- RESTful API for cart management
- Redis cache for cart storage
- Entity Framework Core for data access

## Known Vulnerabilities
This service intentionally contains various security issues for educational purposes:
- Insecure deserialization of cart data
- Missing input validation
- Hardcoded credentials
- Outdated dependencies with known vulnerabilities
- Insecure configuration management
- Missing security headers
- Race conditions in cart updates
- Insecure direct object references
- Insecure session management
- Memory leaks in cart operations

## Setup
```bash
# Build the service
dotnet build

# Run the service
dotnet run
```

## API Documentation
API documentation is available at `/swagger/index.html` when the service is running.

## License
MIT License - See LICENSE file for details

## Security Notice
This application is intentionally vulnerable and should only be used in controlled environments for security testing and education purposes. 
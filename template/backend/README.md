# 🛒 Ambev.DeveloperEvaluation API

## 📌 Project Description

**Ambev.DeveloperEvaluation** API is a RESTful Web API built with **.NET 8** that allows for the management of products, customers, sales, carts, and related operations. It follows clean architecture principles, with a clear separation between domain, application, and infrastructure layers.

This project includes:

- **CQRS + MediatR** for application logic
- **Entity Framework Core** for data persistence
- **FluentValidation** for input validation
- **Unit tests** using xUnit and NSubstitute
- **Docker & Docker Compose** support for containerization
- **Swagger** for interactive API documentation

---

## ▶️ How to Run the Project

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)
- (Optional) [Visual Studio 2022+](https://visualstudio.microsoft.com/) or [Rider](https://www.jetbrains.com/rider/)

---

### 🔧 Running with Docker Compose

1. **Clone the repository**:
   ```bash
   git clone https://github.com/DevGuilhermeSv/mouts-technical-test.git
   cd template/backend
   ```
2. **Build and run the containers:**
    ```bash
       docker-compose up --build
   ```
3. **The API should be accessible at:**
    ````bash
   http://localhost:8080/swagger
   ````
### 🧪 Running Unit Tests

To run the unit tests from the command line:
```bash
    dotnet test
```

Or with coverage (using Coverlet):

```bash
dotnet test /p:CollectCoverage=true

```
---
## 🚀 Technologies Used
.NET 8

- ASP.NET Core Web API

- MediatR

- FluentValidation

- Entity Framework Core

- xUnit, NSubstitute

- Docker & Docker Compose

- Swagger / Swashbuckle
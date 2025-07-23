# Users Microservice

This project is a Users service featuring:
- REST API (Swagger)
- gRPC API
- Auto Database Migration
- Docker Compose orchestration (with PostgreSQL)

---

## 🛠 Prerequisites
- Docker & Docker Compose
- .NET 7+ SDK (for local development/migrations)
- PostgreSQL client (optional, for DB inspection)

---

## 🏗️ How to Build and Run (Dev/Local)

### 1. Build the Project
```bash
docker-compose -f docker-compose.dev.yml build
```

---

### 2. Run Database Migration (Optional)

Migrations are applied automatically on startup if configured. To run manually:

```bash
docker-compose -f docker-compose.dev.yml run users-api dotnet ef database update
```

---

### 3. Run Full Application
```bash
docker-compose -f docker-compose.dev.yml up
```

---

## 🌐 Access the Services

| Service      | URL/Info                                  |
|--------------|-------------------------------------------|
| Swagger UI   | http://localhost:5000/swagger             |
| REST API     | http://localhost:5000/api/users           |
| gRPC         | http://localhost:5000                     |
| Health Check | http://localhost:5000/health              |
| PostgreSQL   | Host: localhost, Port: 5432, User: <user>, DB: <db> |

---

## 🧪 gRPC Proto File for Clients

The gRPC service definition is available at:

```bash
./src/Users.Api/Protos/users.proto
```

> You can generate client code for **C#**, **Go**, **Python**, **Node.js** using this proto file.

Example:
```bash
# C# Client Generation (example using Grpc.Tools)
protoc --csharp_out=. --grpc_out=. --plugin=protoc-gen-grpc=grpc_csharp_plugin.exe users.proto
```

---

## 📦 Docker Compose Services Overview

- **users-api**: The Users API (REST + gRPC)
- **postgres-db**: PostgreSQL Database

Both will be started together using `docker-compose -f docker-compose.dev.yml up`.

---

## 📂 Directory Structure

```bash
/svc-users
  /src
    Users.Api
    Users.Application
    Users.Domain
    Users.Data
    Users.Repositories
    Users.Installment
  /src/Users.Api/Protos/users.proto
  docker-compose.dev.yml
  Dockerfile
  .dockerignore
  README.md
```

---

## ⚙️ Environment Variables
- Configure database connection and other secrets in your environment or via Docker Compose `environment` section.
- Example (see docker-compose.dev.yml):
  - `POSTGRES_USER`, `POSTGRES_PASSWORD`, `POSTGRES_DB`
  - `ConnectionStrings__Default`

---

## 🧹 Static Analysis & Code Quality

This project enforces code quality and style using:
- .NET Built-in Analyzers
- Roslynator.Analyzers & CLI

### Prerequisites
- dotnet-format (install: `dotnet tool install -g dotnet-format`)
- roslynator CLI (install: `dotnet tool install -g roslynator.dotnet.cli`)

### Run All Analyzers & Formatters

**With Makefile (Linux/macOS):**
```sh
make analyze
```

**With PowerShell (Windows):**
```powershell
.\Make.ps1 Analyze
```

### Fixing Violations

- Run the above commands to see all code style and quality issues.
- Use your IDE or editor to fix issues as reported.
- Some issues can be auto-fixed with `dotnet format`, others require manual changes (e.g., XML comments, member ordering).
- You can configure or suppress rules in `.editorconfig` as needed.

---

## 🧪 Testing Strategy

### 2. A. Unit Tests

**Goal:**
Test individual components (e.g., services, handlers, validators) in isolation, using mocks for dependencies.

**What to Test:**
- Application logic in Users.Application (e.g., command/query handlers, validators)
- Domain logic in Users.Domain (e.g., entities, value objects, domain services)
- Repository logic (using in-memory DB or mocks)

**How:**
- Create a test project: tests/Users.UnitTests
- Use xUnit, NUnit, or MSTest (xUnit is most common for .NET Core)
- Use mocking frameworks (e.g., Moq, NSubstitute)
- Test only the logic, not infrastructure or external systems

---

### 2. B. Integration Tests

**Goal:**
Test the system as a whole or in larger slices, including real database, API endpoints, etc.

**What to Test:**
- API endpoints in Users.Api (REST and gRPC)
- Data access with real or test database (e.g., using Testcontainers or Docker Compose)
- End-to-end flows (e.g., create user, fetch user, update user)

**How:**
- Create a test project: tests/Users.IntegrationTests
- Use xUnit/NUnit/MSTest
- Use WebApplicationFactory<T> for ASP.NET Core integration tests
- Use a test database (e.g., SQLite in-memory, or a disposable PostgreSQL container)
- Clean up data between tests

---

### 3. Best Practices
- Isolate unit tests from infrastructure (mock DB, services, etc.).
- Integration tests should use real infrastructure but run in a disposable/test environment.
- Name test projects clearly: Users.UnitTests, Users.IntegrationTests.
- Keep tests fast: unit tests should run in seconds, integration tests can take longer but should be parallelizable.
- Use test data builders or factories to create test objects.
- Clean up after integration tests (reset DB, etc.).
- Run all tests in CI and fail the build on test failures.

---

### 4. Directory Structure

```bash
/svc-users
  /src
    Users.Api
    Users.Application
    Users.Domain
    Users.Data
    Users.Repositories
    Users.Installment
  /tests
    Users.UnitTests
    Users.IntegrationTests
```

---

### 5. Recommended Tools
- **xUnit**: For both unit and integration tests
- **Moq**: For mocking dependencies in unit tests
- **Testcontainers**: For spinning up real DBs in integration tests
- **FluentAssertions**: For expressive assertions

---

### 6. Summary Table

| Test Type        | Scope                | Tools                | DB/Infra   | When to Use         |
|------------------|---------------------|----------------------|------------|--------------------|
| Unit Test        | Single class/method | xUnit, Moq           | Mocked     | All business logic |
| Integration Test | API, DB, end-to-end | xUnit, Testcontainers| Real/Test  | API, DB, flows     |

---

## 🚀 Future Enhancements
- Add Authentication/Authorization
- Centralized logging (Serilog, etc.)
- Health and Readiness probes for Kubernetes

---

# 📦 Quick Notes:

- `dbContext.Database.Migrate()` with **retry** will **ensure migration** happens automatically for Dev/Local.
- **Docker Compose** orchestrates both PostgreSQL and Users API.
- **Proto file** is prepared so you can **generate clients automatically**.
- **Swagger** and **gRPC** are **both exposed on port 5000** (HTTP).
- `.dockerignore` is used to optimize Docker build context.


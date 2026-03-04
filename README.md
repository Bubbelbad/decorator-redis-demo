# decorator-redis-demo

A demo project for testing **HybridCache** and **Redis** in an ASP.NET Core 9 Web API, using the **Decorator pattern** to layer transparent caching on top of a repository.

## What it does

The project exposes a simple REST API for managing customers, backed by PostgreSQL. Caching is applied transparently via the Decorator pattern:

- `ICustomerRepository` — the repository contract
- `CustomerRepository` — the concrete EF Core implementation
- `CachedCustomerRepository` — a decorator that wraps `CustomerRepository` and caches `GetById` results using `HybridCache`

`HybridCache` provides a two-level cache: an in-process memory cache (L1) and a distributed cache (L2), with Redis intended as the distributed backend.

## Tech stack

| | |
|---|---|
| **Framework** | ASP.NET Core 9 |
| **ORM** | Entity Framework Core + Npgsql (PostgreSQL) |
| **Caching** | `Microsoft.Extensions.Caching.Hybrid` + Redis (distributed L2) |
| **API docs** | Scalar (OpenAPI) |
| **Containerization** | Docker Compose (Postgres + app) |

## Running locally

Start the stack with Docker Compose:

```bash
docker compose up
```

The API will be available at `http://localhost:8080`. API documentation is served via Scalar at `/scalar`.

> **Note:** A Postgres password is required at `./secrets/postgres-pwd.txt` before starting.

## Project structure

```
Customers/
  ICustomerRepository.cs        # Repository interface
  CustomerRepository.cs         # EF Core implementation
  CachedCustomerRepository.cs   # Caching decorator (HybridCache)
  CustomersController.cs        # REST API controller
Database/
  DatabaseContext.cs            # EF Core DbContext
```

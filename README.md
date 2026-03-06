# decorator-redis-demo

A demo project for testing **HybridCache** and **Redis** in an ASP.NET Core 9 Web API, using the **Decorator pattern** to layer transparent caching on top of a repository.

## What it does

The project exposes a REST API for managing customers, bikes, companies, and rentals, backed by PostgreSQL. Caching is applied transparently via the Decorator pattern across every domain:

- `IXxxRepository` — the repository contract
- `XxxRepository` — the concrete EF Core implementation
- `CachedXxxRepository` — a decorator that wraps the concrete repository and caches reads using `HybridCache`

`HybridCache` provides a two-level cache: an in-process memory cache (L1) and a distributed Redis cache (L2). Write operations invalidate affected cache entries.

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
Database/
  DatabaseContext.cs            # EF Core DbContext
  BikeEntity.cs
  CustomerEntity.cs
  CompanyEntity.cs
  RentalEntity.cs
  DatabaseExtensionsMethods.cs

Customers/
  ICustomerRepository.cs        # Repository interface
  CustomerRepository.cs         # EF Core implementation
  CachedCustomerRepository.cs   # Caching decorator (HybridCache)
  CustomersController.cs        # REST API controller
  CustomerExtensionMethods.cs   # DI registration

Bikes/
  IBikeRepository.cs
  BikeRepository.cs
  CachedBikeRepository.cs
  BikesController.cs
  BikeExtensionMethods.cs

Companies/
  ICompanyRepository.cs
  CompanyRepository.cs
  CachedCompanyRepository.cs
  CompaniesController.cs
  CompanyExtensionMethods.cs

Rentals/
  IRentalRepository.cs          # GetByIdAsync, StartRentalAsync, EndRentalAsync, GetByCustomerAsync
  RentalRepository.cs
  CachedRentalRepository.cs
  RentalsController.cs
  RentalExtensionMethods.cs
```

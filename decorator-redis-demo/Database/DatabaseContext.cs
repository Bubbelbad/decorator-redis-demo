using Microsoft.EntityFrameworkCore;

namespace DecoratorRedisDemo.Database;

internal class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<BikeEntity> Bikes { get; set; }
}

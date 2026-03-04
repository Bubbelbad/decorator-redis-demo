using Microsoft.EntityFrameworkCore;

namespace DecoratorRedisDemo.Database;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
	public DbSet<CustomerEntity> Customers { get; set; }
	public DbSet<BikeEntity> Bikes { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{

		// Customer
		modelBuilder.Entity<CustomerEntity>()
			.HasKey(e => e.Id);
		modelBuilder.Entity<CustomerEntity>()
			.Property(e => e.Id)
			.ValueGeneratedOnAdd();

		modelBuilder.Entity<BikeEntity>()
			.HasKey(e => e.Id);
		modelBuilder.Entity<BikeEntity>()
			.Property(e => e.Id)
			.ValueGeneratedOnAdd();

	}
}


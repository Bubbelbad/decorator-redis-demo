using Microsoft.EntityFrameworkCore;

namespace DecoratorRedisDemo.Database;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
	public DbSet<CustomerEntity> Customers { get; set; }
	public DbSet<BikeEntity> Bikes { get; set; }
	public DbSet<CompanyEntity> Companies { get; set; }
	public DbSet<RentalEntity> Rentals { get; set; }


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

		// Company
		modelBuilder.Entity<CompanyEntity>()
			.HasKey(e => e.Id);
		modelBuilder.Entity<CompanyEntity>()
			.Property(e => e.Id)
			.ValueGeneratedOnAdd();
		modelBuilder.Entity<CompanyEntity>()
			.HasMany(e => e.Bikes)
			.WithOne(e => e.Company)
			.HasForeignKey(e => e.CompanyId)
			.IsRequired(false)
			.OnDelete(DeleteBehavior.SetNull);

		// Rental
		modelBuilder.Entity<RentalEntity>()
			.HasKey(e => e.Id);
		modelBuilder.Entity<RentalEntity>()
			.Property(e => e.Id)
			.ValueGeneratedOnAdd();
		modelBuilder.Entity<RentalEntity>()
			.HasOne(e => e.Customer)
			.WithMany()
			.HasForeignKey(e => e.CustomerId)
			.IsRequired();
		modelBuilder.Entity<RentalEntity>()
			.HasOne(e => e.Bike)
			.WithMany()
			.HasForeignKey(e => e.BikeId)
			.IsRequired();

	}
}


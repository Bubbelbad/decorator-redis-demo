namespace decorator_redis_demo.Rentals;

public static class RentalExtensionMethods
{
	public static IServiceCollection AddRentals(this IServiceCollection services)
	{
		services.AddScoped<RentalRepository>();
		services.AddScoped<IRentalRepository, CachedRentalRepository>();

		return services;
	}
}

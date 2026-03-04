namespace decorator_redis_demo.Bikes
{
	public static class BikeExtensionMethods
	{
		public static IServiceCollection AddBikes(this IServiceCollection services)
		{
			services.AddScoped<BikeRepository>();
			services.AddScoped<IBikeRepository, CachedBikeRepository>();

			return services;
		}
	}
}

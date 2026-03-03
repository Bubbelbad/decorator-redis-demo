using Microsoft.Extensions.Caching.Memory;

namespace decorator_redis_demo.Customers;

public static class CustomerExtensionMethods
{
	public static IServiceCollection AddCustomer(this IServiceCollection services)
	{
		services.AddMemoryCache();
		services.AddScoped<CustomerRepository>();
		services.AddScoped<ICustomerRepository, CachedCustomerRepository>(); 

		return services; 
	}
}

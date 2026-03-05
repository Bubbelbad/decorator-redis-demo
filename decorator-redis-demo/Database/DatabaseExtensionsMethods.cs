using Microsoft.EntityFrameworkCore;

namespace DecoratorRedisDemo.Database
{
	internal static class DatabaseExtensionsMethods
	{
		public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<DatabaseContext>(opt =>
			{
				var connectionString =
					configuration.GetConnectionString("Db") ??
					throw new InvalidOperationException("Connection string 'Db' not found");

				opt.UseNpgsql(
					connectionString,
					o => o.SetPostgresVersion(18, 0));
			});

			return services; 
		}
	}
}

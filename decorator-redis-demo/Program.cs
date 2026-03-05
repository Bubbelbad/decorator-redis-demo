using decorator_redis_demo.Bikes;
using decorator_redis_demo.Customers;
using DecoratorRedisDemo.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Scalar.AspNetCore;

namespace DecoratorRedisDemo
{
    internal static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

			// Add services
			builder.Services.AddDatabase(builder.Configuration);
			builder.Services.AddCustomer();
			builder.Services.AddBikes();

			builder.Services.AddHybridCache(o =>
			{
				o.DefaultEntryOptions = new HybridCacheEntryOptions
				{
					Expiration = TimeSpan.FromMinutes(5), // Distributed Cache
					LocalCacheExpiration = TimeSpan.FromSeconds(5), // Local IMemoryCache
				};
			});

			builder.Services.AddStackExchangeRedisCache(redisOptions =>
			{
				redisOptions.Configuration = builder.Configuration.GetConnectionString("Redis");
			});

            var app = builder.Build();

            //using (var scope = app.Services.CreateScope())
            //    await scope.ServiceProvider.GetRequiredService<DatabaseContext>().Database.MigrateAsync();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();

				app.MapGet("/", () => Results.Redirect("/scalar"))
					.ExcludeFromDescription(); 
            }

            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}

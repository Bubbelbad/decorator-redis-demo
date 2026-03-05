namespace decorator_redis_demo.Companies;

public static class CompanyExtensionMethods
{
	public static IServiceCollection AddCompanies(this IServiceCollection services)
	{
		services.AddScoped<CompanyRepository>();
		services.AddScoped<ICompanyRepository, CachedCompanyRepository>();

		return services;
	}
}

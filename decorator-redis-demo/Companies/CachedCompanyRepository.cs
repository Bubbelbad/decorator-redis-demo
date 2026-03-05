using DecoratorRedisDemo.Database;
using Microsoft.Extensions.Caching.Hybrid;

namespace decorator_redis_demo.Companies;

public class CachedCompanyRepository : ICompanyRepository
{
	private readonly HybridCache _hybridCache;
	private readonly CompanyRepository _decorated;

	public CachedCompanyRepository(HybridCache cache, CompanyRepository decorated)
	{
		_hybridCache = cache;
		_decorated = decorated;
	}

	public async Task<CompanyEntity?> GetByIdAsync(string id, CancellationToken token) =>
		await _hybridCache.GetOrCreateAsync($"company-{id}", async entry =>
			await _decorated.GetByIdAsync(id, token).ConfigureAwait(false),
			cancellationToken: token).ConfigureAwait(false);

	public async Task<CompanyEntity> AddAsync(string name, CancellationToken token) =>
		await _decorated.AddAsync(name, token).ConfigureAwait(false);

	public async Task<ICollection<BikeEntity>> GetBikesAsync(string id, CancellationToken token) =>
		await _hybridCache.GetOrCreateAsync($"company-{id}-bikes", async entry =>
			await _decorated.GetBikesAsync(id, token).ConfigureAwait(false),
			cancellationToken: token).ConfigureAwait(false);
}

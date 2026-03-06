using DecoratorRedisDemo.Database;
using Microsoft.Extensions.Caching.Hybrid;

namespace decorator_redis_demo.Rentals;

public class CachedRentalRepository : IRentalRepository
{
	private readonly HybridCache _hybridCache;
	private readonly RentalRepository _decorated;

	public CachedRentalRepository(HybridCache hybridCache, RentalRepository decorated)
	{
		_hybridCache = hybridCache;
		_decorated = decorated;
	}

	public async Task<RentalEntity?> GetByIdAsync(string id, CancellationToken token) =>
		await _hybridCache.GetOrCreateAsync($"rental-{id}", async entry =>
			await _decorated.GetByIdAsync(id, token).ConfigureAwait(false),
			cancellationToken: token).ConfigureAwait(false);

	public async Task<RentalEntity> StartRentalAsync(string customerId, string bikeId, CancellationToken token)
	{
		var rental = await _decorated.StartRentalAsync(customerId, bikeId, token).ConfigureAwait(false);
		await _hybridCache.RemoveAsync($"customer-{customerId}-rentals", token).ConfigureAwait(false);
		return rental;
	}

	public async Task<RentalEntity?> EndRentalAsync(string id, CancellationToken token)
	{
		var rental = await _decorated.EndRentalAsync(id, token).ConfigureAwait(false);
		if (rental is not null)
		{
			await _hybridCache.RemoveAsync($"rental-{id}", token).ConfigureAwait(false);
			await _hybridCache.RemoveAsync($"customer-{rental.CustomerId}-rentals", token).ConfigureAwait(false);
		}
		return rental;
	}

	public async Task<ICollection<RentalEntity>> GetByCustomerAsync(string customerId, CancellationToken token) =>
		await _hybridCache.GetOrCreateAsync($"customer-{customerId}-rentals", async entry =>
			await _decorated.GetByCustomerAsync(customerId, token).ConfigureAwait(false),
			cancellationToken: token).ConfigureAwait(false);
}

using DecoratorRedisDemo.Database;
using Microsoft.Extensions.Caching.Hybrid;

namespace decorator_redis_demo.Bikes
{
	public class CachedBikeRepository : IBikeRepository
	{
		private readonly HybridCache _hybridCache;
		private readonly BikeRepository _decorated;

		public CachedBikeRepository(HybridCache cache, BikeRepository decorated)
		{
			_hybridCache = cache;
			_decorated = decorated;
		}

		public async Task<BikeEntity?> AddAsync(string brand, string size, CancellationToken token) =>
				await _decorated.AddAsync(brand, size, token).ConfigureAwait(false);

		public async Task<BikeEntity> GetByIdAsync(string id, CancellationToken token)
		{
			var cachedBike = await _hybridCache.GetOrCreateAsync($"bike-{id}", async entry =>
			{
				var bike = await _decorated.GetByIdAsync(id, token).ConfigureAwait(false);

				return bike;
			}, cancellationToken: token).ConfigureAwait(false);

			return cachedBike;
		}
	}
}

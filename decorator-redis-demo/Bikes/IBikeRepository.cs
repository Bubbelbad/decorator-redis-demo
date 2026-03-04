using DecoratorRedisDemo.Database;

namespace decorator_redis_demo.Bikes
{
	public interface IBikeRepository
	{
		public Task<BikeEntity> GetByIdAsync(string id, CancellationToken token);
		public Task<BikeEntity> AddAsync(string brand, string size, CancellationToken token);

	}
}

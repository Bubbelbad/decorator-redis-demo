using DecoratorRedisDemo.Database;

namespace decorator_redis_demo.Rentals;

public interface IRentalRepository
{
	Task<RentalEntity?> GetByIdAsync(string id, CancellationToken token);
	Task<RentalEntity> StartRentalAsync(string customerId, string bikeId, CancellationToken token);
	Task<RentalEntity?> EndRentalAsync(string id, CancellationToken token);
	Task<ICollection<RentalEntity>> GetByCustomerAsync(string customerId, CancellationToken token);
}

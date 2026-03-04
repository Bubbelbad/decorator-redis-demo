using DecoratorRedisDemo.Database;
using Microsoft.Extensions.Caching.Hybrid;

namespace decorator_redis_demo.Customers;

internal class CachedCustomerRepository : ICustomerRepository
{
	private readonly CustomerRepository _decorated;
	private readonly HybridCache _hybridCache;

	public CachedCustomerRepository(CustomerRepository decorated, HybridCache hybridCache)
	{
		_decorated = decorated;
		_hybridCache = hybridCache;
	}

	public async Task<CustomerEntity> Add(string name, CancellationToken token) =>
		await _decorated.Add(name, token);
	

	public async Task<CustomerEntity?> GetById(string id, CancellationToken token)
	{

		var cachedCustomer = await _hybridCache.GetOrCreateAsync($"customer-{id}", async entry =>
		{
			var customer = await _decorated.GetById(id, token); 

			return customer;
		}, cancellationToken: token).ConfigureAwait(true);

		return cachedCustomer;
	}

	public string Update(CustomerEntity customer)
	{
		throw new NotImplementedException();
	}

	public string Delete(string id)
	{
		throw new NotImplementedException();
	}
}

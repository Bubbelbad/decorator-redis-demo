using DecoratorRedisDemo.Database;
using Microsoft.Extensions.Caching.Memory;

namespace decorator_redis_demo.Customers;

internal class CachedCustomerRepository : ICustomerRepository
{
	private readonly CustomerRepository _decorated;
	private readonly IMemoryCache _memoryCache;

	public CachedCustomerRepository(CustomerRepository decorated, IMemoryCache memoryCache)
	{
		_decorated = decorated;
		_memoryCache = memoryCache; 
	}

	public Task<CustomerEntity> Add(CustomerEntity customer)
	{
		throw new NotImplementedException();
	}

	public Task<CustomerEntity?> GetById(string id)
	{
		string key = $"customer-{id}";

		return _memoryCache.GetOrCreate(
			key,
			entry =>
			{
				entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

				return _decorated.GetById(id);
			});

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

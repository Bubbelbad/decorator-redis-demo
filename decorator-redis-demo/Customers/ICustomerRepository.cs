using DecoratorRedisDemo.Database;

namespace decorator_redis_demo.Customers; 

public interface ICustomerRepository
{
	public Task<CustomerEntity?> GetById(string id, CancellationToken token);
	public Task<CustomerEntity> Add(string name, CancellationToken token);
	public string Update(CustomerEntity customer);
	public string Delete(string id);
}

using DecoratorRedisDemo.Database;

namespace decorator_redis_demo.Customers; 

internal interface ICustomerRepository
{
	public Task<CustomerEntity?> GetById(string id);
	public Task<CustomerEntity> Add(CustomerEntity customer);
	public string Update(CustomerEntity customer);
	public string Delete(string id);
}

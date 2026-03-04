using DecoratorRedisDemo.Database;
using Microsoft.EntityFrameworkCore;

namespace decorator_redis_demo.Customers; 

internal class CustomerRepository : ICustomerRepository
{
	private readonly DatabaseContext _context;

	public CustomerRepository(DatabaseContext context) =>
		_context = context;

	public async Task<CustomerEntity?> GetById(string id, CancellationToken token) =>
		await _context
			.Set<CustomerEntity>()
			.FirstOrDefaultAsync(customer => customer.Id == id).ConfigureAwait(false); 
		//await _context.Customers.FirstAsync(x => x.Id == id).ConfigureAwait(false);

	public async Task<CustomerEntity> Add(string name, CancellationToken token)
	{
		var entry = _context.Customers.Add(new CustomerEntity { Name = name });

		await _context
			.SaveChangesAsync(token)
			.ConfigureAwait(false);

		return entry.Entity;
	}

	string ICustomerRepository.Update(CustomerEntity customer)
	{
		throw new NotImplementedException();
	}

	public string Delete(string id)
	{
		throw new NotImplementedException();
	}
}

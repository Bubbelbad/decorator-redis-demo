using DecoratorRedisDemo.Database;
using Microsoft.AspNetCore.Mvc;

namespace decorator_redis_demo.Customers;

[ApiController]
[Route("[controller]")]
internal class CustomersController : ControllerBase
{
	private readonly ICustomerRepository _repository;

	public CustomersController(ICustomerRepository repository) =>
		_repository = repository;


	[HttpGet("{id}", Name = "GetCustomer")]
	public async Task<ActionResult<CustomerEntity>> Get(string id)
	{
		try
		{
			var customer = await _repository.GetById(id).ConfigureAwait(false);
			return Ok(customer);
		}
		catch (InvalidOperationException)
		{
			return NotFound();
		}
	}

	[HttpPost]
	public async Task<ActionResult<CustomerEntity>> Post([FromBody] CustomerEntity customer)
	{
		var created = await _repository.Add(customer).ConfigureAwait(false);
		return CreatedAtRoute("GetCustomer", new { id = created.Id }, created);
	}

	[HttpPut("{id}")]
	public ActionResult<string> Put(string id, [FromBody] CustomerEntity customer)
	{
		if (!string.Equals(id, customer.Id, StringComparison.Ordinal))
			return BadRequest("Route id does not match customer id");

		var result = _repository.Update(customer);
		return Ok(result);
	}

	[HttpDelete("{id}")]
	public ActionResult<string> Delete(string id)
	{
		var result = _repository.Delete(id);
		return Ok(result);
	}
}

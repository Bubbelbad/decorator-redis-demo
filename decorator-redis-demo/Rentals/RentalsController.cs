using DecoratorRedisDemo.Database;
using Microsoft.AspNetCore.Mvc;

namespace decorator_redis_demo.Rentals;

public record StartRentalRequest(string CustomerId, string BikeId);

[ApiController]
[Route("api/[controller]")]
public class RentalsController : ControllerBase
{
	private readonly IRentalRepository _repository;

	public RentalsController(IRentalRepository repository) =>
		_repository = repository;

	[HttpGet("{id}")]
	public async Task<ActionResult<RentalEntity>> Get(string id, CancellationToken token)
	{
		var rental = await _repository.GetByIdAsync(id, token).ConfigureAwait(false);
		if (rental is null)
			return NotFound();

		return Ok(rental);
	}

	[HttpPost]
	public async Task<ActionResult<RentalEntity>> Post([FromBody] StartRentalRequest request, CancellationToken token)
	{
		var created = await _repository.StartRentalAsync(request.CustomerId, request.BikeId, token).ConfigureAwait(false);
		return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
	}

	[HttpPut("{id}/return")]
	public async Task<ActionResult<RentalEntity>> Return(string id, CancellationToken token)
	{
		var rental = await _repository.EndRentalAsync(id, token).ConfigureAwait(false);
		if (rental is null)
			return NotFound();

		return Ok(rental);
	}

	[HttpGet]
	public async Task<ActionResult<ICollection<RentalEntity>>> GetByCustomer([FromQuery] string customerId, CancellationToken token)
	{
		var rentals = await _repository.GetByCustomerAsync(customerId, token).ConfigureAwait(false);
		return Ok(rentals);
	}
}

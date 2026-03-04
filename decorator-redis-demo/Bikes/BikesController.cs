using DecoratorRedisDemo.Database;
using Microsoft.AspNetCore.Mvc;

namespace decorator_redis_demo.Bikes;
public record CreateBikeRequest(string Brand, string Size);

[ApiController]
[Route("api/[controller]")]
public class BikesController : ControllerBase
{
	private readonly IBikeRepository _repository;

	public BikesController(IBikeRepository repository) =>
		_repository = repository;


	[HttpGet("{id}")]
	public async Task<ActionResult<BikeEntity>> Get(string id, CancellationToken token)
	{
		try
		{
			var bike = await _repository.GetByIdAsync(id, token).ConfigureAwait(false);
			return Ok(bike);
		}
		catch (InvalidOperationException)
		{
			return NotFound();
		}
	}

	[HttpPost]
	public async Task<ActionResult<BikeEntity>> Post([FromBody] CreateBikeRequest request, CancellationToken token)
	{
		var created = await _repository.AddAsync(request.Brand, request.Size, token).ConfigureAwait(false);
		return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
	}
}

using DecoratorRedisDemo.Database;
using Microsoft.AspNetCore.Mvc;

namespace decorator_redis_demo.Companies;

public record CreateCompanyRequest(string Name);

[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
	private readonly ICompanyRepository _repository;

	public CompaniesController(ICompanyRepository repository) =>
		_repository = repository;

	[HttpGet("{id}")]
	public async Task<ActionResult<CompanyEntity>> Get(string id, CancellationToken token)
	{
		var company = await _repository.GetByIdAsync(id, token).ConfigureAwait(false);
		if (company is null)
			return NotFound();

		return Ok(company);
	}

	[HttpPost]
	public async Task<ActionResult<CompanyEntity>> Post([FromBody] CreateCompanyRequest request, CancellationToken token)
	{
		var created = await _repository.AddAsync(request.Name, token).ConfigureAwait(false);
		return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
	}

	[HttpGet("{id}/bikes")]
	public async Task<ActionResult<ICollection<BikeEntity>>> GetBikes(string id, CancellationToken token)
	{
		var company = await _repository.GetByIdAsync(id, token).ConfigureAwait(false);
		if (company is null)
			return NotFound();

		var bikes = await _repository.GetBikesAsync(id, token).ConfigureAwait(false);
		return Ok(bikes);
	}
}

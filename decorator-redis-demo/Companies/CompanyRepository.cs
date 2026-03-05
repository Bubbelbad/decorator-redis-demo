using DecoratorRedisDemo.Database;
using Microsoft.EntityFrameworkCore;

namespace decorator_redis_demo.Companies;

public class CompanyRepository : ICompanyRepository
{
	private readonly DatabaseContext _context;

	public CompanyRepository(DatabaseContext context)
	{
		_context = context;
	}

	public async Task<CompanyEntity?> GetByIdAsync(string id, CancellationToken token) =>
		await _context.Companies.FirstOrDefaultAsync(c => string.Equals(c.Id, id), token).ConfigureAwait(false);

	public async Task<CompanyEntity> AddAsync(string name, CancellationToken token)
	{
		var entry = await _context.Companies.AddAsync(
			new CompanyEntity { Name = name },
			token)
				.ConfigureAwait(true);

		await _context
			.SaveChangesAsync(token)
			.ConfigureAwait(true);

		return entry.Entity;
	}

	public async Task<ICollection<BikeEntity>> GetBikesAsync(string id, CancellationToken token) =>
		await _context.Bikes
			.Where(b => string.Equals(b.CompanyId, id))
			.ToListAsync(token)
			.ConfigureAwait(false);
}

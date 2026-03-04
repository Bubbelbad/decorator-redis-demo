using DecoratorRedisDemo.Database;
using Microsoft.EntityFrameworkCore;

namespace decorator_redis_demo.Bikes;

public class BikeRepository : IBikeRepository
{
	private readonly DatabaseContext _context;

	public BikeRepository(DatabaseContext context)
	{
		_context = context; 
	}

	public async Task<BikeEntity?> AddAsync(string brand, string size, CancellationToken token)
	{
		var entry = await _context.Bikes.AddAsync(
			new BikeEntity { Brand = brand, Size = size },
			token)
				.ConfigureAwait(true);

		await _context
			.SaveChangesAsync(token)
			.ConfigureAwait(true);

		return entry.Entity;
	}

	public async Task<BikeEntity?> GetByIdAsync(string id, CancellationToken token) =>
		await _context.Bikes.FirstOrDefaultAsync(b => string.Equals(b.Id, id), token).ConfigureAwait(false);
}

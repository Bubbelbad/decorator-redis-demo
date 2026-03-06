using DecoratorRedisDemo.Database;
using Microsoft.EntityFrameworkCore;

namespace decorator_redis_demo.Rentals;

public class RentalRepository : IRentalRepository
{
	private readonly DatabaseContext _context;

	public RentalRepository(DatabaseContext context)
	{
		_context = context;
	}

	public async Task<RentalEntity?> GetByIdAsync(string id, CancellationToken token) =>
		await _context.Rentals
			.Include(r => r.Customer)
			.Include(r => r.Bike)
			.FirstOrDefaultAsync(r => string.Equals(r.Id, id), token)
			.ConfigureAwait(false);

	public async Task<RentalEntity> StartRentalAsync(string customerId, string bikeId, CancellationToken token)
	{
		var entry = await _context.Rentals.AddAsync(
			new RentalEntity
			{
				CustomerId = customerId,
				BikeId = bikeId,
				StartDate = DateTime.UtcNow,
				Status = "Active"
			},
			token)
			.ConfigureAwait(true);

		await _context.SaveChangesAsync(token).ConfigureAwait(true);

		return entry.Entity;
	}

	public async Task<RentalEntity?> EndRentalAsync(string id, CancellationToken token)
	{
		var rental = await _context.Rentals
			.FirstOrDefaultAsync(r => string.Equals(r.Id, id), token)
			.ConfigureAwait(false);

		if (rental is null)
			return null;

		rental.EndDate = DateTime.UtcNow;
		rental.Status = "Returned";

		await _context.SaveChangesAsync(token).ConfigureAwait(false);

		return rental;
	}

	public async Task<ICollection<RentalEntity>> GetByCustomerAsync(string customerId, CancellationToken token) =>
		await _context.Rentals
			.Include(r => r.Bike)
			.Where(r => string.Equals(r.CustomerId, customerId))
			.ToListAsync(token)
			.ConfigureAwait(false);
}

using DecoratorRedisDemo.Database;

namespace decorator_redis_demo.Companies;

public interface ICompanyRepository
{
	public Task<CompanyEntity?> GetByIdAsync(string id, CancellationToken token);
	public Task<CompanyEntity> AddAsync(string name, CancellationToken token);
	public Task<ICollection<BikeEntity>> GetBikesAsync(string id, CancellationToken token);
}

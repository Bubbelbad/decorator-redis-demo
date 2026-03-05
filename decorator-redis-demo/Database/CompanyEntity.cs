namespace DecoratorRedisDemo.Database;

public record class CompanyEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Name { get; set; }
    public ICollection<BikeEntity> Bikes { get; set; } = [];
}

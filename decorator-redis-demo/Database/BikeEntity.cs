namespace DecoratorRedisDemo.Database;

public record class BikeEntity
{
	public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Size { get; init; }
    public required string Brand { get; init; }
}

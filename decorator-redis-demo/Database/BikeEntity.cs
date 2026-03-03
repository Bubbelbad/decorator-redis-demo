namespace DecoratorRedisDemo.Database;

internal record class BikeEntity
{
    public required string Id { get; set; }
    public required string Size { get; set; }
    public required string Brand { get; set; }
}

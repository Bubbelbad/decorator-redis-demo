namespace DecoratorRedisDemo.Database;

internal record CustomerEntity
{
    public required string Id { get; set; }
    public required string Name { get; set; }
}

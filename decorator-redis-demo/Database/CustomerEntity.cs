namespace DecoratorRedisDemo.Database;

internal record class CustomerEntity
{
    public required string Id { get; set; }
    public required string Name { get; set; }
}

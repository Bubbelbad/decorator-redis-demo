namespace decorator_redis_demo.Bikes; 

public class Bike
{
	public required string Id { get; init; } = Guid.NewGuid().ToString(); 
	public required string Size { get; set; }
	public required string Brand { get; set; }
}

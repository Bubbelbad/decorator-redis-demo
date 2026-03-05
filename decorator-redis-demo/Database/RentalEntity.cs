namespace DecoratorRedisDemo.Database;

public record class RentalEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string CustomerId { get; set; }
    public CustomerEntity? Customer { get; set; }
    public required string BikeId { get; set; }
    public BikeEntity? Bike { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public required string Status { get; set; } // "Active" | "Returned"
}

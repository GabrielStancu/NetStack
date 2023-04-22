namespace ParticipationService.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public IEnumerable<Order>? Orders { get; set;}
}

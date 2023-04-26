namespace OrderEmailService.Models;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public IEnumerable<int>? Skus { get; set; }
}

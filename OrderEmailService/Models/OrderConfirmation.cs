namespace OrderEmailService.Models;

public class OrderConfirmation
{
    public string Email { get; set; } = string.Empty;
    public Order? Order { get; set; }
}

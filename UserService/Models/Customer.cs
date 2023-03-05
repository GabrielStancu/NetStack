namespace UserService.Models;

public class Customer
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public uint Point { get; set; }
    public bool Deleted { get; set; }

    public User User { get; set; } = default!;
}

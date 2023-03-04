namespace FoodService.Models;

public class Food : ModelBase
{
    public string CategoryCode { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public uint MinQtyDiscount { get; set; }
    public byte[] Image { get; set; } = default!;
}

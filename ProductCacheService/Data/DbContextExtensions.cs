using Bogus;
using ProductCacheService.Models;

namespace ProductCacheService.Data;

public static class DbContextExtensions
{
    public static async Task GenerateProduct(this AppDbContext context, int count)
    {
        var productFaker = new Faker<Product>()
            .RuleFor(prop => prop.Name, setter => setter.Commerce.ProductName())
            .RuleFor(prop => prop.Description, setter => setter.Commerce.ProductDescription())
            .RuleFor(prop => prop.Price, setter => Convert.ToDecimal(setter.Commerce.Price()));

        var products = productFaker.Generate(count);
        await context.Product!.AddRangeAsync(products);
        await context.SaveChangesAsync();
    }
}

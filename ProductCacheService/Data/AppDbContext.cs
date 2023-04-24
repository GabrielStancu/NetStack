using Microsoft.EntityFrameworkCore;
using ProductCacheService.Models;

namespace ProductCacheService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base (options)
    {
    }

    public DbSet<Product>? Product { get; set; }
}

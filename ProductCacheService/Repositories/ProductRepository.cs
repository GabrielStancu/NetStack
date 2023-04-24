using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProductCacheService.Data;
using ProductCacheService.Models;

namespace ProductCacheService.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Product!
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }
}

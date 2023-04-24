using ProductCacheService.Models;

namespace ProductCacheService.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAll(CancellationToken cancellationToken);
}

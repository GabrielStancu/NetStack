using Microsoft.Extensions.Caching.Memory;
using ProductCacheService.Models;

namespace ProductCacheService.Repositories;

public class CacheProductRepository : IProductRepository
{
    private const string ProductListCacheKey = "ProductList";
    private readonly IProductRepository _productRepository;
    private readonly IMemoryCache _memoryCache;

    public CacheProductRepository(IProductRepository productRepository, IMemoryCache mempryCache)
    {
        _productRepository = productRepository;
        _memoryCache = mempryCache;
    }
    public async Task<List<Product>> GetAll(CancellationToken cancellationToken)
    {
        var options = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromSeconds(10))
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(30));

        if (_memoryCache.TryGetValue(ProductListCacheKey, out List<Product> result))
            return result;

        result = await _productRepository.GetAll(cancellationToken);

        _memoryCache.Set(ProductListCacheKey, result, options);

        return result;
    }
}

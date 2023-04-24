using Microsoft.AspNetCore.Mvc;
using ProductCacheService.Repositories;

namespace ProductCacheService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await _productRepository.GetAll(cancellationToken);
        return Ok(response);
    }
}

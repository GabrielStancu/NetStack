using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FruitController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var fruits= await Task.FromResult(new string[] { "apple", "bananana", "kiwi" });
        return Ok(fruits);
    }

    [HttpGet]
    [Route("test")]
    [AllowAnonymous]
    public async Task<IActionResult> Test()
    {
        var fruits = await Task.FromResult(new string[] { "apple", "bananana", "kiwi" });
        return Ok(fruits);
    }
}

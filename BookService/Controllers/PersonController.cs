using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles ="Admin")]
public class PersonController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var fruits= await Task.FromResult(new string[] { "Jack", "Joe", "Jill" });
        return Ok(fruits);
    }
}

using Microsoft.AspNetCore.Mvc;
using ParticipationService.DataSeed;

namespace ParticipationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeedController : ControllerBase
{
    private readonly DataSeeder _seeder;

    public SeedController(DataSeeder seeder)
    {
        _seeder = seeder;
    }

    [HttpGet]
    public ActionResult Seed()
    {
        _seeder.Seed();
        return Ok();
    }
}

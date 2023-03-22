using FeatureService.Services;
using Microsoft.AspNetCore.Mvc;

namespace FeatureService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeatureController : ControllerBase
{
    private readonly ServiceFactory _serviceFactory;

    public FeatureController(ServiceFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }

    [HttpGet("{featureEnabled}")]
    public ActionResult DoSomething(bool featureEnabled)
    {
        // Use the service factory to determine the instance to be created
        var service = _serviceFactory.GetService(featureEnabled);

        service.DoSomething();

        return Ok();
    }
}

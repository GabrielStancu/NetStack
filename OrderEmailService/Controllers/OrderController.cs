using Microsoft.AspNetCore.Mvc;
using OrderEmailService.Models;
using OrderEmailService.Services;

namespace OrderEmailService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IPlaceOrder _placeOrder;

    public OrderController(IPlaceOrder placeOrder)
    {
        _placeOrder = placeOrder;
    }

    [HttpPost]
    public async Task<ActionResult> PlaceOrder([FromBody] Order order)
    {
        await _placeOrder.PlaceOrderAsync(order);
        return Ok();
    }
}

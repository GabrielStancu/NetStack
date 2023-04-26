using System.Threading.Channels;
using OrderEmailService.Models;

namespace OrderEmailService.Services;

public interface IPlaceOrder
{
    Task PlaceOrderAsync(Order order);
}

public class PlaceOrder : IPlaceOrder
{
    public async Task PlaceOrderAsync(Order order)
    {
        var orderChannel = Channel.CreateUnbounded<Order>();
        await orderChannel.Writer.WriteAsync(order);
    }
}

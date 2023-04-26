using System.Threading.Channels;
using OrderEmailService.Models;

namespace OrderEmailService.Services;

public class OrderProcessor : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var orderChannel = Channel.CreateUnbounded<Order>();
        var emailChannel = Channel.CreateUnbounded<OrderConfirmation>();

        while (await orderChannel.Reader.WaitToReadAsync(stoppingToken))
        {
            while (orderChannel.Reader.TryRead(out Order? order))
            {
                if (order is null)
                    continue;

                // Order processing here
                var orderConfirmation = new OrderConfirmation
                {
                    Email = "user@usermail.com",
                    Order = order
                };

                // Send the confirmation email
                await emailChannel.Writer.WriteAsync(orderConfirmation, stoppingToken);
            }
        }
    }
}

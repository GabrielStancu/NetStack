using System.Threading.Channels;
using OrderEmailService.Models;

namespace OrderEmailService.Services;

public class EmailSender : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var emailChannel = Channel.CreateUnbounded<OrderConfirmation>();

        while (await emailChannel.Reader.WaitToReadAsync(stoppingToken))
        {
            while (emailChannel.Reader.TryRead(out OrderConfirmation? orderConfirmation))
            {
                if (orderConfirmation is null || orderConfirmation.Order is null
                    || orderConfirmation.Order.Skus is null)
                {
                    continue;
                }

                // Send the email
                Console.WriteLine($"Hi {orderConfirmation.Email},");
                Console.WriteLine($"Thanks for placing order {orderConfirmation.Order.Id}");
                Console.WriteLine("Items placed: [");

                foreach (var sku in orderConfirmation.Order.Skus)
                {
                    Console.WriteLine(sku);
                }

                Console.WriteLine("]");
            }
        }
    }
}

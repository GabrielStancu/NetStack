using RabbitMQ.Client;

namespace FoodService.MessageBrokerLibrary;

public interface IConnectionProvider : IDisposable
{
    IConnection GetConnection();
}

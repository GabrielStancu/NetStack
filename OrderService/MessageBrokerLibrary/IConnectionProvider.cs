using RabbitMQ.Client;

namespace OrderService.MessageBrokerLibrary;

public interface IConnectionProvider : IDisposable
{
    IConnection GetConnection();
}

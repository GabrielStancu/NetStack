namespace FoodService.MessageBrokerLibrary;

public interface IPublisher : IDisposable
{
    void Publish(
        string message,
        string routingKey,
        IDictionary<string, object>? headers,
        string? timeToLive = null);
}

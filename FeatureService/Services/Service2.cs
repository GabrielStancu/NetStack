namespace FeatureService.Services;

public class Service2 : IService
{
    public void DoSomething()
    {
        Console.WriteLine($"Writing from {nameof(Service2)}");
    }
}

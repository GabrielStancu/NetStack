namespace FeatureService.Services;

public class Service1 : IService
{
    public void DoSomething()
    {
        Console.WriteLine($"Writing from {nameof(Service1)}");
    }
}

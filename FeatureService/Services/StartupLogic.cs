namespace FeatureService.Services;

public class StartupLogic : IStartupLogic
{
    public void WarmUp()
    {
        Console.WriteLine($"Running warmup tasks at program startup from {nameof(StartupLogic)}");
    }
}

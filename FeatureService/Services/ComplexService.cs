namespace FeatureService.Services;

public class ComplexService : IComplexService
{
    private readonly IServiceProvider _serviceProvider;

    public ComplexService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void DoSomethingComplex()
    {
        using var scope1 = _serviceProvider.CreateScope();
        var writeStorage = scope1.ServiceProvider.GetRequiredService<IWriteStorage>();
        // Use the service
        // Once the scope within the service was created ends, the service is disposed too

        using var scope2 = _serviceProvider.CreateScope();
        var readStorage = scope2.ServiceProvider.GetRequiredService<IReadStorage>();
        // Use the service
        // Once the scope within the service was created ends, the service is disposed too
    }
}

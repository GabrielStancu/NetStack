namespace FeatureService.Services;

public class ServiceFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ServiceFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IService GetService(bool featureEnabled)
    {
        return featureEnabled switch
        {
            true => _serviceProvider.GetRequiredService<Service1>(),
            _ => _serviceProvider.GetRequiredService<Service2>()
        };
    }
}

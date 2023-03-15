namespace ToDoService.Extensions;

public static class ScopeFactoryExtensions
{
    // The DI using EF Core will create a connection / session or scope
    // If we try to work in parallel on the context, we fail since we use the same context
    // We use this method to create different sessions / scopes within the original request
    // The T parameter is the scoped instance needed by the Func to run
    public static async Task DoAsync<T>(this IServiceScopeFactory serviceScope, Func<T, Task> work)
    {
        using var scope = serviceScope.CreateScope();
        T tVar = scope.ServiceProvider.GetRequiredService<T>();
        await work(tVar);
    }
}

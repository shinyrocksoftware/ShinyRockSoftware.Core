using Microsoft.Extensions.DependencyInjection;

namespace Core.Caching.Memory.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInMemoryCache(this IServiceCollection services)
    {
        return services.AddMemoryCache();
    }
}
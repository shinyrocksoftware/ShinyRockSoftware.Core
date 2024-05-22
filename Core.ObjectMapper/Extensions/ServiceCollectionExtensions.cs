using Core.ObjectMapper.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace Core.ObjectMapper.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMapper(this IServiceCollection services, params Assembly?[] assemblies)
    {
        assemblies = assemblies is not null && assemblies.Any() ? assemblies : [Assembly.GetEntryAssembly()];
        foreach (var assembly in assemblies)
        {
            var profileTypes = assembly?.GetTypes().Where(t => t.IsSubclassOfGeneric(typeof(MapperProfile<,>)) && !t.IsAbstract);

            if (profileTypes != null)
            {
                foreach (var profile in profileTypes)
                {
                    Activator.CreateInstance(profile);
                }
            }
        }

        return services;
    }
}
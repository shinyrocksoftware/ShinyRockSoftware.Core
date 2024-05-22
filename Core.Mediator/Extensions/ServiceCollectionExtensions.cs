using Microsoft.Extensions.DependencyInjection;

namespace Core.Mediator.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddMediators(this IServiceCollection services, IEnumerable<Assembly>? assemblies = null)
	{
		if (assemblies == null)
		{
			string? assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

			if (assemblyFolder != null)
			{
				assemblies = Directory.GetFiles(assemblyFolder, "*.dll").Select(Assembly.LoadFrom);
			}
		}

		services.AddMediatR(cfg =>
		{
			if (assemblies != null)
			{
				foreach (var assembly in assemblies)
				{
					cfg.RegisterServicesFromAssembly(assembly);
				}
			}
		});

		return services;
	}
}
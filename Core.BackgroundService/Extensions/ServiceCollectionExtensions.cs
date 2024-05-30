using Core.BackgroundService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Core.BackgroundService.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddPeriodicBackgroundService<T>(this IServiceCollection services)
		where T : class, IPeriodicBackgroundService
	{
		return services.AddSingleton<T>()
		               .AddHostedService(provider => provider.GetRequiredService<T>());
	}

	public static IServiceCollection AddCronBackgroundService<T>(this IServiceCollection services)
		where T : class, ICronBackgroundService
	{
		return services.AddSingleton<T>()
		               .AddHostedService(provider => provider.GetRequiredService<T>());
	}
}
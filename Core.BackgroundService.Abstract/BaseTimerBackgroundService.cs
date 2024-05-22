using Core.Helper.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Core.BackgroundService.Abstract;

public abstract class BaseTimerBackgroundService : Microsoft.Extensions.Hosting.BackgroundService
{
	protected readonly IServiceScopeFactory ServiceScopeFactory;
	protected readonly ILogger Logger;
	protected readonly IDateTimeHelper DateTimeHelper;

	public bool IsInitialization { get; set; } = true;
	public int ExecutionCount { get; set; }
	public bool IsEnabled { get; set; } = true;

	public abstract string ServiceName { get; }
	public abstract Task DoWorkAsyncDelegate(IServiceProvider serviceProvider, CancellationToken stoppingToken);

	protected BaseTimerBackgroundService(IServiceScopeFactory serviceScopeFactory)
	{
		ServiceScopeFactory = serviceScopeFactory;

		using var asyncScope = ServiceScopeFactory.CreateAsyncScope();
		Logger = asyncScope.ServiceProvider.GetRequiredService<ILogger<BaseTimerBackgroundService>>();
		DateTimeHelper = asyncScope.ServiceProvider.GetRequiredService<IDateTimeHelper>();
	}
}
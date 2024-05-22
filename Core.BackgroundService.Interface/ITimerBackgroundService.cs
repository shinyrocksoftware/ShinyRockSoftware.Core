using Microsoft.Extensions.Hosting;

namespace Core.BackgroundService.Interface;

public interface ITimerBackgroundService : IHostedService, IDisposable
{
	bool IsInitialization { get; set; }
	int ExecutionCount { get; set; }
	bool IsEnabled { get; set; }
	string ServiceName { get; }
	Task DoWorkAsyncDelegate(IServiceProvider serviceProvider, CancellationToken stoppingToken);
}
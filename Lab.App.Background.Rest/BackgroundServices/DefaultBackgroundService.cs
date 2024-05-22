using Core.BackgroundService.Abstract;
using Core.BackgroundService.Interface;
using Core.Extension;

namespace Lab.App.Background.Rest.BackgroundServices;

public class DefaultBackgroundService(IServiceScopeFactory serviceScopeFactory)
	: BasePeriodicBackgroundService(serviceScopeFactory), IPeriodicBackgroundService
{
	public override string ServiceName => "default_background_service";
	public override TimeSpan Period => TimeSpan.FromSeconds(5);

	public override async Task DoWorkAsyncDelegate(IServiceProvider serviceProvider, CancellationToken stoppingToken)
	{
		await Task.CompletedTask;
		Logger.LogInformation($"Working at {DateTime.Now.ToVietnamString()}");
	}
}
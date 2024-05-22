using Core.BackgroundService.Abstract;
using Core.BackgroundService.Interface;
using Core.Extension;

namespace Lab.App.Background;

public class PeriodicBackgroundService(IServiceScopeFactory factory)
	: BasePeriodicBackgroundService(factory), IPeriodicBackgroundService
{
	public override string ServiceName => "default_periodic_background_service";
	public override TimeSpan Period => TimeSpan.FromSeconds(5);

	public override async Task DoWorkAsyncDelegate(IServiceProvider serviceProvider, CancellationToken stoppingToken)
	{
		await Task.CompletedTask;
		Logger.LogInformation("Do Periodic\'s work at " + DateTime.Now.ToVietnamString());
	}
}
using Base.Extension;
using Core.BackgroundService.Abstract;
using Core.BackgroundService.Interface;

namespace Lab.App.Background;

public class CronBackgroundService(IServiceScopeFactory factory)
	: BaseCronBackgroundService(factory), ICronBackgroundService
{
	public override string ServiceName => "default_cron_background_service";
	public override string CronExpression => "5 0 0 ? * * *";

	public override async Task DoWorkAsyncDelegate(IServiceProvider serviceProvider, CancellationToken stoppingToken)
	{
		await Task.CompletedTask;
		Logger.LogInformation("Do Cron\'s work at " + DateTime.Now.ToVietnamString());
	}
}
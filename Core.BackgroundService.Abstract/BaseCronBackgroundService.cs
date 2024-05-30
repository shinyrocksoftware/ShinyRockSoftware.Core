using Base.Extension;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Core.BackgroundService.Abstract;

public abstract class BaseCronBackgroundService(IServiceScopeFactory serviceScopeFactory)
	: BaseTimerBackgroundService(serviceScopeFactory)
{
	public abstract string CronExpression { get; }

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		if (IsInitialization)
		{
			Logger.LogInformation("CronBackgroundService {HeaderKey} started, restart trigger on cron is {Value}", ServiceName, CronExpression);
			IsInitialization = false;
		}

		while (!stoppingToken.IsCancellationRequested)
		{
			try
			{
				if (IsEnabled)
				{
					var expression = new CronExpression(CronExpression);
					DateTimeOffset? timer = expression.GetTimeAfter(DateTimeHelper.GetUtcNowOffset());

					if (timer >= DateTimeHelper.GetUtcNowOffset())
					{
						await using var asyncScope = ServiceScopeFactory.CreateAsyncScope();
						await DoWorkAsyncDelegate(asyncScope.ServiceProvider, stoppingToken);
					}

					Logger.LogInformation("{HeaderKey} restarts at {Value}", ServiceName, DateTimeHelper.GetNow().ToVietnamString());
					ExecutionCount++;
				}
			}
			catch (Exception ex)
			{
				Logger.LogInformation(ex.Message, ex);
			}

			Thread.Sleep(1000);
		}
	}
}
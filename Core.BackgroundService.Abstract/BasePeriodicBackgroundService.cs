using Core.Extension;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Core.BackgroundService.Abstract;

public abstract class BasePeriodicBackgroundService(IServiceScopeFactory serviceScopeFactory)
    : BaseTimerBackgroundService(serviceScopeFactory)
{
    public abstract TimeSpan Period { get; }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (IsInitialization)
        {
            Logger.LogInformation("PeriodicBackgroundService {HeaderKey} started, restart period is {Value}", ServiceName, Period);
            IsInitialization = false;
        }

        using var timer = new PeriodicTimer(Period);

        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                if (IsEnabled)
                {
                    await using var asyncScope = ServiceScopeFactory.CreateAsyncScope();
                    await DoWorkAsyncDelegate(asyncScope.ServiceProvider, stoppingToken);

                    Logger.LogInformation("{HeaderKey} restarts at {Value}", ServiceName, DateTimeHelper.GetNow().ToVietnamString());
                    ExecutionCount++;
                }
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex.Message, ex);
            }
        }
    }
}
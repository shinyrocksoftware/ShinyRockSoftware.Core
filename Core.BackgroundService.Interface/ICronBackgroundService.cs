namespace Core.BackgroundService.Interface;

public interface ICronBackgroundService : ITimerBackgroundService
{
	string CronExpression { get; }
}
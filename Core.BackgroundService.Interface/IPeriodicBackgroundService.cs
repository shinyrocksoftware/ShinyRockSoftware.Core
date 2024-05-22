namespace Core.BackgroundService.Interface;

public interface IPeriodicBackgroundService : ITimerBackgroundService
{
	TimeSpan Period { get; }
}
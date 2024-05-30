namespace Core.BackgroundService.Models;

public record ServiceStatus(string ServiceName, bool IsRunning, int ExecutionCount);
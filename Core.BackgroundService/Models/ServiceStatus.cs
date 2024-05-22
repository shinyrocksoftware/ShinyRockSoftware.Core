namespace Core.Job.Models;

public record ServiceStatus(string ServiceName, bool IsRunning, int ExecutionCount);
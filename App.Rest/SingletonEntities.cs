using System.Diagnostics;

namespace App.Rest;

public class SingletonEntities
{
	public static PerformanceCounter CpuCounter = new("Process", "% Processor Time", Process.GetCurrentProcess().ProcessName);
	public static PerformanceCounter MemoryCounter = new("Memory", "Available MBytes");
}
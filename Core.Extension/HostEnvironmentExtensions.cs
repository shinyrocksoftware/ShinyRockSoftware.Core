using Microsoft.Extensions.Hosting;

namespace Core.Extension;

public static class HostEnvironmentExtensions
{
	public static bool IsLocalDevelopment(this IHostEnvironment hostEnvironment)
	{
		return hostEnvironment.IsLocal() || hostEnvironment.IsEnvironment(Environments.Development);
	}

	public static bool IsLocal(this IHostEnvironment hostEnvironment)
	{
		return hostEnvironment.IsEnvironment("Local");
	}
}
using Core.Extension;

namespace Core.Helper;

public class EnvironmentHelper
{
	public static readonly bool IsLocal = "Local".IsAspNetCoreEnvironmentVariable();
	public static readonly bool IsDevelopment = "Development".IsAspNetCoreEnvironmentVariable();
}
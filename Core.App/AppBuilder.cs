using Core.Configuration.Extensions;
using Core.Logger.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Core.App;

public static class AppBuilder
{
	///  <summary>
	///  Create default console application including:
	///  <list type="bullet">
	///  <item>
	/// 		<description>Logger</description>
	///  </item>
	///  <item>
	/// 		<description>AutoInject</description>
	///  </item>
	///  </list>
	///  </summary>
	///  <param name="appRootNamespaces"></param>
	///  <param name="extendingDelegate"></param>
	///  <param name="moreConfig"></param>
	///  <param name="args"></param>
	///  <returns></returns>
	public static IHost CreateDefaultConsoleApplication(
		string[] appRootNamespaces
		, Action<IConfiguration, IServiceCollection>? extendingDelegate
		, Func<IHost, IHost>? moreConfig = null
		, string[]? args = null
	)
	{
		var hostBuilder = Host.CreateDefaultBuilder(args)
		                      .ConfigureServices((hostingContext, services) =>
		                      {
			                      var configuration = hostingContext.Configuration;

			                      services.AddServiceBase(configuration)
			                              .AutoInject(appRootNamespaces)
			                              .AddDefaultLogger(configuration);

			                      extendingDelegate?.Invoke(configuration, services);
		                      })
		                      .Build();

		if (moreConfig != null)
		{
			hostBuilder = moreConfig.Invoke(hostBuilder);
		}

		return hostBuilder;
	}
}
using Core.App;
using Core.BackgroundService.Extensions;
using Core.BackgroundService.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace App.Background.Abstract;

public abstract class BaseBackgroundProgram
{
	protected void RunDefaultPeriodic<T>(
		string[] appRootNamespaces
		, Action<IConfiguration, IServiceCollection>? extendingDelegate = null
		, string[]? args = null)
		where T : class, IPeriodicBackgroundService
	{
		AppBuilder.CreateDefaultConsoleApplication(appRootNamespaces, (configuration, services) =>
		          {
			          services.AddPeriodicBackgroundService<T>();
			          extendingDelegate?.Invoke(configuration, services);
		          }, null, args)
		          .Run();
	}

	protected void RunDefaultCron<T>(
		string[] appRootNamespaces
		, Action<IConfiguration, IServiceCollection>? extendingDelegate = null
		, string[]? args = null)
		where T : class, ICronBackgroundService
	{
		AppBuilder.CreateDefaultConsoleApplication(appRootNamespaces, (configuration, services) =>
		          {
			          services.AddCronBackgroundService<T>();
			          extendingDelegate?.Invoke(configuration, services);
		          }, null, args)
		          .Run();
	}
}
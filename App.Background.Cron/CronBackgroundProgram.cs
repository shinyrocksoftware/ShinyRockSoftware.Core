using App.Background.Abstract;
using Core.BackgroundService.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Background.Cron;

public class CronBackgroundProgram : BaseBackgroundProgram
{
	public void Run<T>(
		string[] appRootNamespaces
		, Action<IConfiguration, IServiceCollection>? extendingDelegate = null
		, string[]? args = null)
		where T : class, ICronBackgroundService
	{
		RunDefaultCron<T>(appRootNamespaces, extendingDelegate, args);
	}
}
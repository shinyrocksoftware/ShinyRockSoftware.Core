using App.Background.Abstract;
using Core.BackgroundService.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Background.Periodic;

public class PeriodicBackgroundProgram : BaseBackgroundProgram
{
	public void Run<T>(
		string[] appRootNamespaces
		, Action<IConfiguration, IServiceCollection>? extendingDelegate = null
		, string[]? args = null)
		where T : class, IPeriodicBackgroundService
	{
		RunDefaultPeriodic<T>(appRootNamespaces, extendingDelegate, args);
	}
}
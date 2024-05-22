using Core.Rds.PostgreSql.Extensions;
using App.Rest.Abstract;
using Core.App;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Rest.PostgreSql;

public class RestPostgreSqlProgram : BaseRestProgram
{
	public void Run(
		string[] appRootNamespaces
		, FeatureOptions featureOptions
		, Action<MvcOptions>? mvcOptionsDelegate = null
		, Func<IMvcCoreBuilder, IMvcCoreBuilder>? mvcBuilderDelegate = null
		, Action<IConfiguration, IServiceCollection>? extendingDelegate = null
		, Action<IServiceCollection, IConfiguration>? customHealthChecksDelegate = null
		, Action<WebApplicationBuilder, FeatureOptions>? customLoggerDelegate = null
		, int restPort = 5000
		, string[]? args = null
	)
	{
		RunDefault(
			appRootNamespaces
			, featureOptions
			, mvcOptionsDelegate
			, mvcBuilderDelegate
			, (configuration, services) =>
			{
				if (featureOptions.UseRds)
				{
					services.AddPostgreSqlRdsDb(configuration);
				}

				extendingDelegate?.Invoke(configuration, services);
			}, customHealthChecksDelegate
			, customLoggerDelegate
			, restPort
			, args: args);
	}
}
using Core.Extension;
using Core.Constant;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Core.App;
using Microsoft.AspNetCore.Mvc;

namespace App.Rest.Abstract;

public abstract class BaseRestProgram
{
	public Func<Assembly[]> MappingAssembliesQuery { get; set; }

	protected void RunDefault(
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
		CreateRestWebApplication(
				appRootNamespaces
				, featureOptions
				, mvcOptionsDelegate
				, mvcBuilderDelegate
				, extendingDelegate
				, null
				, customHealthChecksDelegate
				, customLoggerDelegate
				, restPort
				, args)
			.Run();
	}

	/// <summary>
	/// The application could be set port by pasting the restPort variable, or the environment variable REST_PORT
	/// </summary>
	/// <param name="restPort"></param>
	/// <param name="appRootNamespaces"></param>
	/// <param name="featureOptions"></param>
	/// <param name="mvcOptionsDelegate"></param>
	/// <param name="extendingDelegate"></param>
	/// <param name="mvcBuilderDelegate"></param>
	/// <param name="moreBuilderConfigDelegate"></param>
	/// <param name="customHealthChecksDelegate"></param>
	/// <param name="customLoggerDelegate"></param>
	/// <param name="args"></param>
	/// <returns></returns>
	private WebApplication CreateRestWebApplication(
		string[] appRootNamespaces
		, FeatureOptions featureOptions
		, Action<MvcOptions>? mvcOptionsDelegate = null
		, Func<IMvcCoreBuilder, IMvcCoreBuilder>? mvcBuilderDelegate = null
		, Action<IConfiguration, IServiceCollection>? extendingDelegate = null
		, Func<WebApplicationBuilder, WebApplicationBuilder>? moreBuilderConfigDelegate = null
		, Action<IServiceCollection, IConfiguration>? customHealthChecksDelegate = null
		, Action<WebApplicationBuilder, FeatureOptions>? customLoggerDelegate = null
		, int restPort = 0
		, string[]? args = null
	)
	{
		var app = WebBuilder.CreateDefaultWebApplication(
			appRootNamespaces
			, featureOptions
			, mvcOptionsDelegate
			, mvcBuilderDelegate
			, MappingAssembliesQuery
			, extendingDelegate
			, builder =>
			{
				builder.WebHost.ConfigureKestrel(options =>
				{
					options.AddServerHeader = false;

					var restPortEnv = WebConstants.REST_PORT.GetOptionalEnvironmentVariable();
					if (restPortEnv.IsNotNullNorEmpty())
					{
						restPort = restPortEnv.ToInt();
					}

					options.ListenLocalhost(restPort, o => o.Protocols = HttpProtocols.Http1);
				});

				if (moreBuilderConfigDelegate != null)
				{
					builder = moreBuilderConfigDelegate.Invoke(builder);
				}

				return builder;
			}
			, customHealthChecksDelegate
			, customLoggerDelegate
			, null
			, args);

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapDefaultControllerRoute();
		});

		return app;
	}
}
using Base.Constant;
using Base.Extension;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Core.App;
using Core.Helper;
using Microsoft.AspNetCore.Mvc;

namespace App.Grpc.Abstract;

public abstract class BaseGrpcProgram
{
	public Func<Assembly[]> MappingAssembliesQuery { get; set; }

	protected void RunDefault(
		string[] appRootNamespaces
		, FeatureOptions featureOptions
		, Action<IEndpointRouteBuilder> mapGrpcEndpointsDelegate
		, Action<MvcOptions>? mvcOptionsDelegate = null
		, Func<IMvcCoreBuilder, IMvcCoreBuilder>? mvcBuilderDelegate = null
		, Action<IConfiguration, IServiceCollection>? extendingDelegate = null
		, Action<IServiceCollection, IConfiguration>? customHealthChecksDelegate = null
		, int restPort = 5001
		, int grpcPort = 5002
		, string[]? args = null
	)
	{
		CreateDefaultGrpcWebApplication(
				appRootNamespaces
				, featureOptions
				, mapGrpcEndpointsDelegate
				, mvcOptionsDelegate
				, mvcBuilderDelegate
				, extendingDelegate
				, null
				, customHealthChecksDelegate
				, restPort
				, grpcPort
				, args)
			.Run();
	}

	///  <summary>
	/// 	The application could be set port by pasting the restPort, grpcPort variables, or the environment variable REST_PORT, GRPC_PORT
	///  </summary>
	///  <param name="appRootNamespaces"></param>
	///  <param name="featureOptions"></param>
	///  <param name="mapGrpcEndpointsDelegate"></param>
	///  <param name="restPort"></param>
	///  <param name="grpcPort"></param>
	///  <param name="mvcBuilderDelegate"></param>
	///  <param name="extendingDelegate"></param>
	///  <param name="moreBuilderConfigDelegate"></param>
	///  <param name="customHealthChecksDelegate"></param>
	///  <param name="args"></param>
	///  <param name="mvcOptionsDelegate"></param>
	///  <returns></returns>
	private WebApplication CreateDefaultGrpcWebApplication(
		string[] appRootNamespaces
		, FeatureOptions featureOptions
		, Action<IEndpointRouteBuilder> mapGrpcEndpointsDelegate
		, Action<MvcOptions>? mvcOptionsDelegate = null
		, Func<IMvcCoreBuilder, IMvcCoreBuilder>? mvcBuilderDelegate = null
		, Action<IConfiguration, IServiceCollection>? extendingDelegate = null
		, Func<WebApplicationBuilder, WebApplicationBuilder>? moreBuilderConfigDelegate = null
		, Action<IServiceCollection, IConfiguration>? customHealthChecksDelegate = null
		, int restPort = 0
		, int grpcPort = 0
		, string[]? args = null
	)
	{
		var app = WebBuilder.CreateDefaultWebApplication(
			appRootNamespaces
			, featureOptions
			, mvcOptionsDelegate
			, mvcBuilderDelegate
			, MappingAssembliesQuery
			, (configuration, services) =>
			{
				services.AddGrpc().AddJsonTranscoding();

				if (EnvironmentHelper.IsLocal || EnvironmentHelper.IsDevelopment)
				{
					services.AddGrpcSwagger();
				}

				extendingDelegate?.Invoke(configuration, services);
			}, builder =>
			{
				builder.WebHost.ConfigureKestrel(options =>
				{
					options.AddServerHeader = false;

					var restPortEnv = WebConstants.REST_PORT.GetOptionalEnvironmentVariable();
					if (restPortEnv.IsNotNullNorEmpty())
					{
						restPort = restPortEnv.ToInt();
					}

					var grpcPortEnv = WebConstants.GRPC_PORT.GetOptionalEnvironmentVariable();
					if (grpcPortEnv.IsNotNullNorEmpty())
					{
						grpcPort = grpcPortEnv.ToInt();
					}

					options.ListenLocalhost(restPort, o => o.Protocols = HttpProtocols.Http1);
					options.ListenLocalhost(grpcPort, o => o.Protocols = HttpProtocols.Http2);
				});

				if (moreBuilderConfigDelegate != null)
				{
					builder = moreBuilderConfigDelegate.Invoke(builder);
				}

				return builder;
			}
			, customHealthChecksDelegate
			, (c, xmlPath) =>
			{
				c.IncludeGrpcXmlComments(xmlPath, includeControllerXmlComments: true);
			}
			, args);

		app.UseEndpoints(mapGrpcEndpointsDelegate);

		return app;
	}
}
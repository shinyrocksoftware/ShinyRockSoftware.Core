using Base.Extension;
using Core.App.Middlewares;
using Core.App.ModelConventions;
using Core.Attribute.ActionFilters;
using Core.Caching.Redis.Extensions;
using Core.Configuration.Extensions;
using Core.Configuration.Factories;
using Core.Doc.Swashbuckle.Extensions;
using Core.Logger.Extensions;
using Core.Mediator.Extensions;
using Core.ObjectMapper.Extensions;
using Core.Stream.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Core.App;

public static class WebBuilder
{
	///  <summary>
	///  Create default web application including:
	///  <list type="bullet">
	///  <item>
	/// 		<description>ResponseCaching</description>
	///  </item>
	///  <item>
	/// 		<description>DistributedMemoryCache</description>
	///  </item>
	///  <item>
	/// 		<description>AutoInject</description>
	///  </item>
	///  <item>
	/// 		<description>Mediator</description>
	///  </item>
	///  <item>
	/// 		<description>MvcCore</description>
	///  </item>
	///  <item>
	/// 		<description>ApiExplorer</description>
	///  </item>
	///  <item>
	/// 		<description>Authorization</description>
	///  </item>
	///  <item>
	/// 		<description>Cors</description>
	///  </item>
	///  <item>
	/// 		<description>Mapper</description>
	///  </item>
	///  <item>
	/// 		<description>Kafka</description>
	///  </item>
	///  <item>
	/// 		<description>Redis</description>
	///  </item>
	///  <item>
	/// 		<description>ErrorHandlingMiddleware</description>
	///  </item>
	///  <item>
	/// 		<description>Healthcheck</description>
	///  </item>
	///  <item>
	/// 		<description>Routing</description>
	///  </item>
	///  <item>
	/// 		<description>Authorization on Local environment</description>
	///  </item>
	///  <item>
	/// 		<description>ExceptionHandler and Hsts on non-Local environment</description>
	///  </item>
	///  </list>
	///  </summary>
	///  <param name="appRootNamespaces"></param>
	///  <param name="featureOptions"></param>
	///  <param name="mvcOptionsDelegate"></param>
	///  <param name="mvcBuilderDelegate"></param>
	///  <param name="mappingAssembliesQuery"></param>
	///  <param name="extendingDelegate"></param>
	///  <param name="moreBuilderConfigDelegate"></param>
	///  <param name="customHealthChecksDelegate"></param>
	///  <param name="extendingOptions"></param>
	///  <param name="args"></param>
	///  <returns></returns>
	public static WebApplication CreateDefaultWebApplication(
		string[] appRootNamespaces
		, FeatureOptions featureOptions
		, Action<MvcOptions>? mvcOptionsDelegate = null
		, Func<IMvcCoreBuilder, IMvcCoreBuilder>? mvcBuilderDelegate = null
		, Func<Assembly[]>? mappingAssembliesQuery = null
		, Action<IConfiguration, IServiceCollection>? extendingDelegate = null
		, Func<WebApplicationBuilder, WebApplicationBuilder>? moreBuilderConfigDelegate = null
		, Action<IServiceCollection, IConfiguration>? customHealthChecksDelegate = null
		, Action<SwaggerGenOptions, string>? extendingOptions = null
		, string[]? args = null
	)
	{
		var builder = WebApplication.CreateBuilder(args ?? []);

		var configuration = builder.Configuration;
		var services = builder.Services;

		services
			.AddHttpContextAccessor()
			.AddResponseCaching()
			.AddDistributedMemoryCache()
			.AddServiceBase(configuration)
			.AddMediators()
			.AutoInject(appRootNamespaces)
			.AddMapper(mappingAssembliesQuery?.Invoke())
			.AddDefaultSwagger(extendingOptions);

		if (featureOptions.UseKafka)
		{
			services
				.AddKafka(configuration);
		}

		if (featureOptions.UseRedis)
		{
			services
				.AddRedis(configuration);
		}

		var mvcBuilder = services
			.AddMvcCore(options =>
			{
				options.Conventions.Add(new KebabCaseRoutingConvention());

				options.Filters.Add<AttachRequestIdToResponseActionFilterAttribute>();
				options.Filters.Add<DateTimeUtcValidationActionFilterAttribute>();
				options.Filters.Add<PayloadTrimStringActionFilterAttribute>();
				options.Filters.Add<PayloadTypeValidationActionFilterAttribute>();
				options.Filters.Add<PayloadValidationActionFilterAttribute>();

				mvcOptionsDelegate?.Invoke(options);
			})
			.AddApiExplorer()
			.AddAuthorization()
			.AddCors()
			.AddDataAnnotations()
			.ConfigureApiBehaviorOptions(options =>
			{
				options.SuppressModelStateInvalidFilter = true;
				options.SuppressMapClientErrors = true;
			});

		mvcBuilderDelegate?.Invoke(mvcBuilder);

		if (customHealthChecksDelegate == null)
		{
			services.AddHealthChecks();
		}
		else
		{
			customHealthChecksDelegate(services, configuration);
		}

		builder = builder.AddLogger();
		builder = moreBuilderConfigDelegate?.Invoke(builder);
		extendingDelegate?.Invoke(configuration, services);

		// Normal WebAPI stuff
		var app = builder.Build();

		app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin())
		   .UseMiddleware<ErrorHandlingMiddleware>()
		   .UseHealthChecks("/healthcheck")
		   .UseRouting()
		   .UseHttpLogging()
		   .UseAuthorization();

		if (app.Environment.IsLocalDevelopment())
		{
			app.UseDefaultSwagger();
		}

		if (!app.Environment.IsLocal())
		{
			ValidateConnectorModels();

			app.UseExceptionHandler("/error")
			   .UseHttpsRedirection()
			   .UseHsts();
		}

		return app;
	}

	/// <summary>
	/// Validate the Connector Models at startup
	/// </summary>
	/// <exception cref="Exception"></exception>
	private static void ValidateConnectorModels()
	{
		var result = new StringBuilder();
		var isValid = true;

		foreach (var action in ConfigurationValidationFactory.RemoteActions)
		{
			try
			{
				action();
			}
			catch (Exception e)
			{
				result.AppendLine(e.Message);
				isValid = false;
			}
		}

		if (!isValid)
		{
			throw new(result.ToString());
		}
	}
}
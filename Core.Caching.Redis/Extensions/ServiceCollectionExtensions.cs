using Core.Caching.Redis.ConfigurationConnectors;
using Core.Caching.Redis.ConnectorModels;
using Core.Caching.Redis.Managers;
using Core.Caching.Redis.Models;
using Core.Configuration.Extensions;
using Core.Configuration.Interface;
using Base.Constant;
using Core.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.System.Text.Json;
using IConfigurationManager = Core.Configuration.Interface.IConfigurationManager;

namespace Core.Caching.Redis.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
	{
		services = EnvironmentHelper.IsLocal
			? services.AddConnectorModelDependencies<RedisConnectorModel, RedisConfigurationConnector>(configuration, SettingConstants.APP_SETTING_REDIS)
			: services.AddConnectorModelDependencies<RedisConnectorModel, RedisRemoteConfigurationConnector>();

		var connectorModelHelper = services.BuildServiceProvider().GetRequiredService<IConnectorModelHelper>();
		var connectorModel = connectorModelHelper.GetConnector<RedisConnectorModel>();

		if (connectorModel.IsValid)
		{
			services
				.AddStackExchangeRedis()
				.AddStackExchangeRedisCache(options =>
				{
					options.Configuration = "localhost";
					options.InstanceName = "SampleInstance";
				});

			services
				.AddScoped<IConfigurationManager>(c => ActivatorUtilities.CreateInstance<RedisConfigurationManager>(c, SettingConstants.APP_SETTING_REDIS));

			var healthChecksBuilder = services.AddHealthChecks();

			var redisConnectorModel = services.GetConnectorModel<RedisConnectorModel>();
			healthChecksBuilder.AddRedis(redisConnectorModel.ConnectionString);
		}

		return services;
	}

	/// <summary>
	/// Add StackExchangeRedis dependencies
	/// </summary>
	/// <param name="services"></param>
	/// <returns></returns>
	private static IServiceCollection AddStackExchangeRedis(this IServiceCollection services)
	{
		var connectorModelHelper = services.BuildServiceProvider().GetRequiredService<IConnectorModelHelper>();
		var connectorModel = connectorModelHelper.GetConnector<RedisConnectorModel>();

		var redisConfiguration = new RedisConfiguration
		{
			Hosts =
			[
				new RedisHost
				{
					Host = connectorModel.Host
					, Port = connectorModel.Port
				}
			]
			, ConnectTimeout = 200
			, SyncTimeout = 200
			, KeyPrefix = connectorModel.Prefix
		};
		var configurationOptions = redisConfiguration.ConfigurationOptions;

		services.AddStackExchangeRedisExtensions<SystemTextJsonSerializer>(redisConfiguration)
		        .Configure<RedisCacheOptions>(opt =>
		        {
			        opt.ConfigurationOptions = configurationOptions;
		        })
		        .AddScoped(_ => ConnectionMultiplexer.Connect(configurationOptions))
		        .AddSingleton(redisConfiguration);

		return services;
	}
}
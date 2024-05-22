using Core.Configuration.Extensions;
using Core.Constant;
using Core.Helper;
using Core.Stream.ConfigurationConnectors;
using Core.Stream.ConnectorModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Stream.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddKafka(this IServiceCollection services, IConfiguration configuration)
	{
		return EnvironmentHelper.IsLocal
			? services.AddConnectorModelDependencies<KafkaConnectorModel, KafkaConfigurationConnector>(configuration, SettingConstants.APP_SETTING_KAFKA)
			: services.AddConnectorModelDependencies<KafkaConnectorModel, KafkaRemoteConfigurationConnector>();
	}
}
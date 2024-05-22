using Core.Configuration.Interface;
using Core.Configuration.ConnectorModels;
using Core.Configuration.Extensions;
using Core.Constant;
using Core.Helper;
using Core.Logger.ConfigurationConnectors;
using Core.Logger.ConnectorModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Core.Logger.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDefaultLogger(this IServiceCollection services, IConfiguration configuration)
    {
	    Console.OutputEncoding = Encoding.UTF8;

	    var serviceProvider = services.BuildServiceProvider();
	    var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
	    var connectorModelHelper = serviceProvider.GetRequiredService<IConnectorModelHelper>();
	    var serviceConnectorModel = connectorModelHelper.GetConnector<ServiceConnectorModel>();

	    var logger = loggerFactory.CreateLogger(serviceConnectorModel.Code);
	    services.TryAddSingleton(logger);

        return EnvironmentHelper.IsLocal
            ? services.AddConnectorModelDependencies<LoggerConnectorModel, LoggerConfigurationConnector>(configuration, SettingConstants.APP_SETTING_LOGGER)
            : services.AddConnectorModelDependencies<LoggerConnectorModel, LoggerRemoteConfigurationConnector>();
    }
}
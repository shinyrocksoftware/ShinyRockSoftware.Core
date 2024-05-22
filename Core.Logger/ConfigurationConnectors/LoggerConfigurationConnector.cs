using Core.Configuration.ConfigurationConnectors;
using Core.Logger.ConnectorModels;
using Microsoft.Extensions.Options;

namespace Core.Logger.ConfigurationConnectors;

public class LoggerConfigurationConnector(IOptions<LoggerConnectorModel> options)
	: ConfigurationConnector<LoggerConnectorModel>(options);
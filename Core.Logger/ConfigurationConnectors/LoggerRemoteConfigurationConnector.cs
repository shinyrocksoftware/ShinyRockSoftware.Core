using Core.Configuration.Interface;
using Core.Configuration.ConfigurationConnectors;
using Core.Logger.ConnectorModels;

namespace Core.Logger.ConfigurationConnectors;

public class LoggerRemoteConfigurationConnector(IConfigurationManager configurationManager)
	: RemoteConfigurationConnector<LoggerConnectorModel>(configurationManager);
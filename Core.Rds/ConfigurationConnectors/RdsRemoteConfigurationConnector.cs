using Core.Configuration.ConfigurationConnectors;
using Core.Configuration.Interface;
using Core.Rds.ConnectorModels;

namespace Core.Rds.ConfigurationConnectors;

public class RdsRemoteConfigurationConnector(IConfigurationManager configurationManager)
	: RemoteConfigurationConnector<RdsConnectorModel>(configurationManager);
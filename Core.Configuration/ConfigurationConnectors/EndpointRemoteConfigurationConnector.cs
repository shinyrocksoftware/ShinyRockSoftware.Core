using Core.Configuration.ConnectorModels;
using Core.Configuration.Interface;

namespace Core.Configuration.ConfigurationConnectors;

public class EndpointRemoteConfigurationConnector(IConfigurationManager configurationManager)
	: RemoteConfigurationConnector<EndpointConnectorModel>(configurationManager);
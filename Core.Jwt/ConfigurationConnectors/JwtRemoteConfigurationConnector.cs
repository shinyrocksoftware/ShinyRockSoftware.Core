using Core.Configuration.Interface;
using Core.Configuration.ConfigurationConnectors;
using Core.Jwt.ConnectorModels;

namespace Core.Jwt.ConfigurationConnectors;

public class JwtRemoteConfigurationConnector(IConfigurationManager configurationManager)
	: RemoteConfigurationConnector<JwtConnectorModel>(configurationManager);
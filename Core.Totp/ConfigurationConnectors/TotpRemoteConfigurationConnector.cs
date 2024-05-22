using Core.Configuration.Interface;
using Core.Configuration.ConfigurationConnectors;
using Core.Totp.ConnectorModels;

namespace Core.Totp.ConfigurationConnectors;

public class TotpRemoteConfigurationConnector(IConfigurationManager configurationManager)
	: RemoteConfigurationConnector<TotpConnectorModel>(configurationManager);
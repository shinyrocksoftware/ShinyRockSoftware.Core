using Core.Configuration.ConfigurationConnectors;
using Core.Totp.ConnectorModels;
using Microsoft.Extensions.Options;

namespace Core.Totp.ConfigurationConnectors;

public class TotpConfigurationConnector(IOptions<TotpConnectorModel> options)
	: ConfigurationConnector<TotpConnectorModel>(options);
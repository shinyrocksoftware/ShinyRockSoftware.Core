using Core.Configuration.ConfigurationConnectors;
using Core.Jwt.ConnectorModels;
using Microsoft.Extensions.Options;

namespace Core.Jwt.ConfigurationConnectors;

public class JwtConfigurationConnector(IOptions<JwtConnectorModel> options)
	: ConfigurationConnector<JwtConnectorModel>(options);
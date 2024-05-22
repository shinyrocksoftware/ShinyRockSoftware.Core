using Core.Configuration.ConnectorModels;
using Microsoft.Extensions.Options;

namespace Core.Configuration.ConfigurationConnectors;

public class EndpointConfigurationConnector(IOptions<EndpointConnectorModel> options)
	: ConfigurationConnector<EndpointConnectorModel>(options);
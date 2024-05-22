using Core.Configuration.ConnectorModels;
using Microsoft.Extensions.Options;

namespace Core.Configuration.ConfigurationConnectors;

public class ServicesConfigurationConnector(IOptions<ServiceConnectorModel> options)
	: ConfigurationConnector<ServiceConnectorModel>(options);
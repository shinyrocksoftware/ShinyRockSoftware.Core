using Core.Configuration.ConfigurationConnectors;
using Core.Rds.ConnectorModels;
using Microsoft.Extensions.Options;

namespace Core.Rds.ConfigurationConnectors;

public class RdsConfigurationConnector(IOptions<RdsConnectorModel> options)
	: ConfigurationConnector<RdsConnectorModel>(options);
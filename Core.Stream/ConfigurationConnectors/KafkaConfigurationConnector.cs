using Core.Configuration.ConfigurationConnectors;
using Core.Stream.ConnectorModels;
using Microsoft.Extensions.Options;

namespace Core.Stream.ConfigurationConnectors;

public class KafkaConfigurationConnector(IOptions<KafkaConnectorModel> options)
	: ConfigurationConnector<KafkaConnectorModel>(options);
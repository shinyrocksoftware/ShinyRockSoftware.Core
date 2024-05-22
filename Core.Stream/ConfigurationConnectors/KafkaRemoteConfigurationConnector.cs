using Core.Configuration.Interface;
using Core.Configuration.ConfigurationConnectors;
using Core.Stream.ConnectorModels;

namespace Core.Stream.ConfigurationConnectors;

public class KafkaRemoteConfigurationConnector(IConfigurationManager configurationManager)
	: RemoteConfigurationConnector<KafkaConnectorModel>(configurationManager);
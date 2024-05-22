using Core.Configuration.ConfigurationConnectors;
using Core.Caching.Redis.ConnectorModels;
using Microsoft.Extensions.Options;

namespace Core.Caching.Redis.ConfigurationConnectors;

public class RedisConfigurationConnector(IOptions<RedisConnectorModel> options)
	: ConfigurationConnector<RedisConnectorModel>(options);
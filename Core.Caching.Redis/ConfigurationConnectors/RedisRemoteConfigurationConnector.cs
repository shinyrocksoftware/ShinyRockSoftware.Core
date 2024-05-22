using Core.Configuration.ConfigurationConnectors;
using Core.Caching.Redis.ConnectorModels;
using Core.Configuration.Interface;
using Core.Extension;
using Core.Model;

namespace Core.Caching.Redis.ConfigurationConnectors;

public class RedisRemoteConfigurationConnector(IConfigurationHelpers configurationHelpers)
    : RemoteConfigurationConnector<RedisConnectorModel>(null)
{
    protected override IDictionary<string, string> CachedConfigurations
    {
        get
        {
            if (LockModel.Configurations.IsNullOrEmpty())
            {
                lock (LockModel.LockObject)
                {
                    if (LockModel.Configurations.IsNullOrEmpty())
                    {
                        LockModel.Configurations = configurationHelpers.GetConfigurations();
                    }
                }
            }

            return LockModel.Configurations;
        }
    }
}
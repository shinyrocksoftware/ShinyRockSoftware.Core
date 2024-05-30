using Base.Constant;

namespace Core.Configuration.ConfigurationConnectors;

public abstract class BaseConfigurationConnector
{
    public static readonly string EndpointConfig = Environment.GetEnvironmentVariable(GeneralConstants.ENDPOINT_CONFIG);
}
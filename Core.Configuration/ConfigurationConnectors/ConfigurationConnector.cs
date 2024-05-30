using Core.Model.Abstract.ConnectorModels;
using Base.Model.Interface;
using Microsoft.Extensions.Options;

namespace Core.Configuration.ConfigurationConnectors;

public class ConfigurationConnector : BaseConfigurationConnector, IConfigurationConnector
{
    protected readonly BaseValidationModel ConnectorModel;

    public ConfigurationConnector(IOptions<BaseValidationModel> options)
    {
        ConnectorModel = options.Value;

        if (!ConnectorModel.IsValid)
        {
            throw new(ConnectorModel.InvalidMessage);
        }
    }

    public BaseValidationModel GetConnector()
    {
        return ConnectorModel;
    }

    public object GetConnector(Type type)
    {
        return ConnectorModel;
    }
}

public class ConfigurationConnector<T> : BaseConfigurationConnector, IConfigurationConnector<T>
    where T : class, IConnectorModel, new()
{
    protected readonly T ConnectorModel;

    public ConfigurationConnector(IOptions<T> options)
    {
        ConnectorModel = options.Value;

        if (!ConnectorModel.IsValid)
        {
            throw new(ConnectorModel.InvalidMessage);
        }
    }

    public T GetConnector()
    {
        return ConnectorModel;
    }
}
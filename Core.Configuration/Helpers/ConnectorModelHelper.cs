using Core.Attribute.AutoInjection;
using Core.Configuration.Interface;
using Core.Model.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Configuration.Helpers;

[SingletonAutoInjection]
internal class ConnectorModelHelper : IConnectorModelHelper
{
    private readonly IServiceProvider _serviceProvider;

    public ConnectorModelHelper(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public T GetConnector<T>() where T : class, IConnectorModel
    {
        var serviceConfigurationConnector = _serviceProvider.GetService<IConfigurationConnector<T>>();
        return serviceConfigurationConnector?.GetConnector();
    }
}
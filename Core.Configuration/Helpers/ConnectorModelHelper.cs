using Core.Attribute.AutoInjection;
using Core.Configuration.Interface;
using Base.Model.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Configuration.Helpers;

[SingletonAutoInjection]
internal class ConnectorModelHelper(IServiceProvider serviceProvider) : IConnectorModelHelper
{
    public T GetConnector<T>() where T : class, IConnectorModel
    {
        var serviceConfigurationConnector = serviceProvider.GetService<IConfigurationConnector<T>>();
        return serviceConfigurationConnector?.GetConnector();
    }
}
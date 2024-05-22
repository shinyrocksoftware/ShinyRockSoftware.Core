using Core.Model.Interface;

namespace Core.Configuration.Interface;

public interface IConnectorModelHelper : IAutoInjection
{
    T GetConnector<T>() where T : class, IConnectorModel;
}
namespace Base.Model.Interface;

public interface IConfigurationConnector
{
    object GetConnector(Type type);
}

public interface IConfigurationConnector<T> where T : IConnectorModel
{
    T GetConnector();
}
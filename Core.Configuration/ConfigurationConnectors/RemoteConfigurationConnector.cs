using Base.Extension;
using Core.Attribute;
using Core.Configuration.Factories;
using Core.Configuration.Interface;
using Core.Model;
using Core.Model.Abstract.ConnectorModels;
using Base.Model.Interface;

namespace Core.Configuration.ConfigurationConnectors;

public class RemoteConfigurationConnector(IConfigurationManager configurationManager)
    : BaseConfigurationConnector, IConfigurationConnector
{
    protected virtual IDictionary<string, string> CachedConfigurations
    {
        get
        {
            if (LockModel.Configurations.IsNullOrEmpty())
            {
                lock (LockModel.LockObject)
                {
                    if (LockModel.Configurations.IsNullOrEmpty())
                    {
                        LockModel.Configurations = configurationManager?.GetConfigurations();
                    }
                }
            }

            return LockModel.Configurations == null ? new Dictionary<string, string>() : LockModel.Configurations;
        }
    }

    public object GetConnector(Type type)
    {
        var connectorModel = Activator.CreateInstance(type);

        return SetValue(type, connectorModel);
    }

    protected object SetValue(Type classType, object connectorModel, Action<string>? errorAction = null)
    {
        var properties = classType.GetProperties();

        foreach (var property in properties)
        {
            var singleKeyConnectorModelAttribute = property.GetAttribute<SingleKeyConnectorModelAttribute>();
            if (singleKeyConnectorModelAttribute != null)
            {
                var key = singleKeyConnectorModelAttribute.Value;
                var required = singleKeyConnectorModelAttribute.Required;

                bool isSuccess = CachedConfigurations.TryGetValue(key, out string value);
                if (isSuccess)
                {
                    bool setString = true;

                    if (singleKeyConnectorModelAttribute.IsInt)
                    {
                        isSuccess = int.TryParse(value, out var intValue);
                        if (isSuccess)
                        {
                            property.SetValue(connectorModel, intValue);
                            setString = false;
                        }
                    }
                    else if (singleKeyConnectorModelAttribute.IsBoolean)
                    {
                        isSuccess = bool.TryParse(value, out var boolValue);
                        if (isSuccess)
                        {
                            property.SetValue(connectorModel, boolValue);
                            setString = false;
                        }
                    }
                    else if (singleKeyConnectorModelAttribute.IsTimeSpan)
                    {
                        isSuccess = TimeSpan.TryParse(value, out var timeSpanValue);
                        if (isSuccess)
                        {
                            property.SetValue(connectorModel, timeSpanValue);
                            setString = false;
                        }
                    }
                    else if (singleKeyConnectorModelAttribute.IsEnumerable)
                    {
                        var deserialize = value.Deserialize();
                        if (deserialize != null)
                        {
                            property.SetValue(connectorModel, deserialize);
                            setString = false;
                        }
                    }

                    if (setString)
                    {
                        property.SetValue(connectorModel, value);
                    }
                }
                else
                {
                    if (required)
                    {
                        throw new((connectorModel as BaseValidationModel)?.InvalidMessage);
                    }
                }
            }
            else
            {
                var multipleKeyConnectorModelAttribute = property.GetAttribute<CompositeKeyConnectorModelAttribute>();
                if (multipleKeyConnectorModelAttribute != null)
                {
                    var innerClassType = property.PropertyType;
                    var innerConnectorModel = Activator.CreateInstance(innerClassType);
                    innerConnectorModel = SetValue(innerClassType, innerConnectorModel, errorAction);

                    property.SetValue(connectorModel, innerConnectorModel);
                }
            }
        }

        return connectorModel;
    }
}

public class RemoteConfigurationConnector<T> : BaseConfigurationConnector, IConfigurationConnector<T>
    where T : IConnectorModel, new()
{
    private readonly IConfigurationManager _configurationManager;

    protected virtual IDictionary<string, string> CachedConfigurations
    {
        get
        {
            if (LockModel.Configurations.IsNullOrEmpty())
            {
                lock (LockModel.LockObject)
                {
                    if (LockModel.Configurations.IsNullOrEmpty())
                    {
                        LockModel.Configurations = _configurationManager?.GetConfigurations();
                    }
                }
            }

            return LockModel.Configurations == null
                ? new Dictionary<string, string>()
                : LockModel.Configurations;
        }
    }

    public RemoteConfigurationConnector(IConfigurationManager configurationManager)
    {
        _configurationManager = configurationManager;

        ConfigurationValidationFactory.RemoteActions.Add(() =>
        {
            GetConnector();
        });
    }

    public T GetConnector()
    {
        var connectorModel = Activator.CreateInstance<T>();

        return (T) SetValue(typeof(T), connectorModel);
    }

    public T GetConnector(Type type)
    {
        var connectorModel = Activator.CreateInstance(type);

        return (T) SetValue(type, connectorModel);
    }

    protected object SetValue(Type classType, object connectorModel)
    {
        var properties = classType.GetProperties();

        foreach (var property in properties)
        {
            var singleKeyConnectorModelAttribute = property.GetAttribute<SingleKeyConnectorModelAttribute>();
            if (singleKeyConnectorModelAttribute != null)
            {
                var key = singleKeyConnectorModelAttribute.Value;
                var required = singleKeyConnectorModelAttribute.Required;

                bool isSuccess = CachedConfigurations.TryGetValue(key, out string value);
                if (isSuccess)
                {
                    bool setString = true;

                    if (singleKeyConnectorModelAttribute.IsInt)
                    {
                        isSuccess = int.TryParse(value, out var intValue);
                        if (isSuccess)
                        {
                            property.SetValue(connectorModel, intValue);
                            setString = false;
                        }
                    }
                    else if (singleKeyConnectorModelAttribute.IsBoolean)
                    {
                        isSuccess = bool.TryParse(value, out var boolValue);
                        if (isSuccess)
                        {
                            property.SetValue(connectorModel, boolValue);
                            setString = false;
                        }
                    }
                    else if (singleKeyConnectorModelAttribute.IsTimeSpan)
                    {
                        isSuccess = TimeSpan.TryParse(value, out var timeSpanValue);
                        if (isSuccess)
                        {
                            property.SetValue(connectorModel, timeSpanValue);
                            setString = false;
                        }
                    }
                    else if (singleKeyConnectorModelAttribute.IsEnumerable)
                    {
                        var deserialize = value.Deserialize();
                        if (deserialize != null)
                        {
                            property.SetValue(connectorModel, deserialize);
                            setString = false;
                        }
                    }

                    if (setString)
                    {
                        property.SetValue(connectorModel, value);
                    }
                }
                else
                {
                    if (required)
                    {
                        throw new(((BaseValidationModel) connectorModel).InvalidMessage);
                    }
                }
            }
            else
            {
                var multipleKeyConnectorModelAttribute = property.GetAttribute<CompositeKeyConnectorModelAttribute>();
                if (multipleKeyConnectorModelAttribute != null)
                {
                    var innerClassType = property.PropertyType;
                    var innerConnectorModel = Activator.CreateInstance(innerClassType);
                    innerConnectorModel = SetValue(innerClassType, innerConnectorModel);

                    property.SetValue(connectorModel, innerConnectorModel);
                }
            }
        }

        return connectorModel;
    }
}
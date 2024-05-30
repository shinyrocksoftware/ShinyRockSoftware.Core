using System.ComponentModel;
using Base.Model.Interface;

namespace Core.Attribute;

public class SingleKeyConnectorModelAttribute : DefaultValueAttribute, IAttribute<string>
{
    public SingleKeyConnectorModelAttribute(string value, Type? type = null, bool required = true) : base(value)
    {
        Value = value;
        Required = required;
        Type = type;

        if (type != null)
        {
            IsInt = type == typeof(int);
            IsBoolean = type == typeof(bool);
            IsTimeSpan = type == typeof(TimeSpan);
            IsEnumerable = type.IsGenericType;
        }
    }

    public new string Value { get; }
    public bool Required { get; }
    public Type Type { get; }
    public bool IsInt { get; }
    public bool IsBoolean { get; }
    public bool IsTimeSpan { get; }
    public bool IsEnumerable { get; }
}
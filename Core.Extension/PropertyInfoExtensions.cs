namespace Core.Extension;

public static class PropertyInfoExtensions
{
    public static T GetAttribute<T>(this PropertyInfo property) where T : Attribute
    {
        return (T) Attribute.GetCustomAttribute(property, typeof(T));
    }
}
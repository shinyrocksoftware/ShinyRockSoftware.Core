namespace Base.Extension;

public static class PropertyInfoExtensions
{
    public static T? GetAttribute<T>(this PropertyInfo property) where T : Attribute
    {
        return Attribute.GetCustomAttribute(property, typeof(T)) as T;
    }
}
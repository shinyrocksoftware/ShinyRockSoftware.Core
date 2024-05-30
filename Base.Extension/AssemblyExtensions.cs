namespace Base.Extension;

public static class AssemblyExtensions
{
    public static bool IsSubclassOfGeneric(this Type sourceType, Type genericType)
    {
        var result = false;

        if (sourceType != null && sourceType != typeof(object) && sourceType.BaseType != null)
        {
            var typeGeneric = sourceType.IsGenericType ? sourceType.GetGenericTypeDefinition() : sourceType;
            result = genericType == typeGeneric || IsSubclassOfGeneric(sourceType.BaseType, genericType);
        }

        return result;
    }
}
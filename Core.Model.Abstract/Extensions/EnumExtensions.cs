using Base.Model.Interface;

namespace Core.Model.Abstract.Extensions;

public static class EnumExtensions
{
    public static TV AttributeText<T, TV>(this System.Enum source)
    {
        return GetAttribute<T, TV>(source);
    }

    public static TV GetAttribute<T, TV>(this System.Enum source)
    {
        var result = default(TV);

        if (source != null)
        {
            var fieldInfo = source.GetType().GetField(source.ToString());
            if (fieldInfo != null)
            {
                if (fieldInfo.GetCustomAttributes(typeof(T), false) is T[] attributes && attributes.Any())
                {
                    if (attributes[0] is IAttribute<TV> attribute)
                    {
                        result = attribute.Value;
                    }
                }
            }
        }

        return result;
    }
}
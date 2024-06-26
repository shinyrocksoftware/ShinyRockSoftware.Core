using Omu.ValueInjecter;

namespace Base.ObjectMapper.Extension;

public static class CollectionExtensions
{
    public static IEnumerable<TV> Map<T, TV>(this IEnumerable<T> source)
    {
        return source == null ? [] : source.Select(item => Mapper.Map<TV>(item));
    }
        
    public static ICollection<TV> Map<T, TV>(this ICollection<T> source)
    {
        return Map<T, TV>(source.AsEnumerable()).ToList();
    }
        
    public static IList<TV> Map<T, TV>(this IList<T> source)
    {
        return Map<T, TV>(source.AsEnumerable()).ToList();
    }
}
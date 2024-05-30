namespace Base.Extension;

public static class DictionaryExtensions
{
    /// <summary>
    /// Parse to JsonString
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string ToJsonString(this IDictionary<string, string> source)
    {
        var entries = source.Select(d => $"\"{d.Key}\": \"{string.Join(",", d.Value)}\"");
        return $"{{{string.Join(",", entries)}}}";
    }

    public static Dictionary<string, T> ToDictionary<T>(this IDictionary<string, T> source)
    {
        return source.ToDictionary(c => c.Key, c => c.Value);
    }
}
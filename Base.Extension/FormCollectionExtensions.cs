using Microsoft.AspNetCore.Http;

namespace Base.Extension;

public static class FormCollectionExtensions
{
    public static Dictionary<string, string?> ToJsonDictionary(this IFormCollection source)
    {
        return source.Keys.ToDictionary<string, string, string?>(key => key, key => source[key]);
    }
}
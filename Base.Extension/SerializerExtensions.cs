using System.Text.Json;
using System.Text.Json.Serialization;

namespace Base.Extension;

public static class SerializerExtensions
{
    public static string Serialize<T>(this T obj)
    {
        return JsonSerializer.Serialize(obj, SerializerOptions);
    }

    public static T? Deserialize<T>(this string? strObject)
    {
        return strObject.IsNullOrEmpty()
            ? default
            : JsonSerializer.Deserialize<T>(strObject, SerializerOptions);
    }

    public static JsonElement? Deserialize(this string strObject)
    {
        JsonElement? result = null;

        try
        {
            result = (JsonElement?) JsonSerializer.Deserialize(strObject, typeof(JsonElement), SerializerOptions);
        }
        catch (JsonException)
        {
        }

        return result;
    }

    public static async Task<T?> DeserializeAsync<T>(this string strObject)
    {
        T? result = default;

        if (strObject.IsNullOrEmpty())
        {
            result = await Task.FromResult(default(T));
        }
        else
        {
            await strObject.ActionInStreamAsync(async stream =>
            {
                result = await JsonSerializer.DeserializeAsync<T>(stream, SerializerOptions);
            });
        }

        return result;
    }

    public static async Task<T?> DeserializeAsync<T>(this string strObject, Type baseType) where T : class
    {
        T? result = default;

        try
        {
            await strObject.ActionInStreamAsync(async stream =>
            {
                result = await JsonSerializer.DeserializeAsync(stream, baseType, SerializerOptions) as T;
            });
        }
        catch (JsonException)
        {
        }

        return result;
    }

    public static async Task<JsonElement?> DeserializeAsync(this string strObject)
    {
        JsonElement? result = null;

        try
        {
            await strObject.ActionInStreamAsync(async stream =>
            {
                result = (JsonElement?) await JsonSerializer.DeserializeAsync(stream, typeof(JsonElement), SerializerOptions);
            });
        }
        catch (JsonException)
        {
        }

        return result;
    }

    private static JsonSerializerOptions SerializerOptions =>
        new()
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        };
}
using System.Text.Json;

namespace Base.Extension;

public static class ByteArrayExtensions
{
    public static string BinaryToBase64(this byte[] byteArray)
    {
        return Convert.ToBase64String(byteArray);
    }

    public static string BinaryToImageBase64(this byte[] bytes, string contentType)
    {
        return $"data:{contentType};base64,{BinaryToBase64(bytes)}";
    }

    public static string ToString(this byte[] source)
    {
        return BitConverter.ToString(source);
    }

    public static string ToMySqlUuidString(this byte[] source)
    {
        return ToString(source).ToMySqlUuidString();
    }

    public static T To<T>(this byte[] bytes, int offset = 0)
    {
        var result = default(T);

        var type = typeof(T);
        if (type == typeof(sbyte))
        {
            result = ((sbyte) bytes[offset]).As<T>();
        }
        else if (type == typeof(byte))
        {
            result = bytes[offset].As<T>();
        }
        else if (type == typeof(short))
        {
            result = BitConverter.ToInt16(bytes, offset).As<T>();
        }
        else if (type == typeof(ushort))
        {
            result = BitConverter.ToUInt16(bytes, offset).As<T>();
        }
        else if (type == typeof(int))
        {
            result = BitConverter.ToInt32(bytes, offset).As<T>();
        }
        else if (type == typeof(uint))
        {
            result = BitConverter.ToUInt32(bytes, offset).As<T>();
        }
        else if (type == typeof(long))
        {
            result = BitConverter.ToInt64(bytes, offset).As<T>();
        }
        else if (type == typeof(ulong))
        {
            result = BitConverter.ToUInt64(bytes, offset).As<T>();
        }
        else if (typeof(T) == typeof(IDictionary<string, string>) || typeof(T) == typeof(Dictionary<string, string>))
        {
            using var stream = new MemoryStream();

            stream.Write(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);

            result = JsonSerializer.Deserialize<T>(stream);
        }
        else if (typeof(T) == typeof(string))
        {
            result = Encoding.UTF8.GetString(bytes, offset, bytes.Length).As<T>();
        }

        return result;
    }
}
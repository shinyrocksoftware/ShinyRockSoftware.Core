namespace Base.Extension;

public static class GuidExtensions
{
    public static string ToMySqlUuidString(this Guid source)
    {
        return source.ToString().ToMySqlUuidString();
    }

    public static bool IsNullOrEmpty(this Guid source)
    {
        return source == Guid.Empty;
    }

    public static bool IsNullOrEmpty(this Guid? source)
    {
        return source == null || source.Value == Guid.Empty;
    }

    public static bool IsNotNullNorEmpty(this Guid source)
    {
        return !source.IsNullOrEmpty();
    }

    public static bool IsNotNullNorEmpty(this Guid? source)
    {
        return !source.IsNullOrEmpty();
    }

    public static uint ToUint(this Guid source)
    {
        byte[] b = source.ToByteArray();
        return BitConverter.ToUInt32(b, 0);
    }
}
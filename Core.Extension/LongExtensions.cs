namespace Core.Extension;

public static class LongExtensions
{
    public static DateTime ToDateTime(this long unixDate)
    {
        var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return start.AddSeconds(unixDate);
    }
}
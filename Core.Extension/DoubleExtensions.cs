namespace Core.Extension;

public static class DoubleExtensions
{
    public static DateTime ToDateTime(this double unixTimeStamp)
    {
        var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        return dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
    }
}
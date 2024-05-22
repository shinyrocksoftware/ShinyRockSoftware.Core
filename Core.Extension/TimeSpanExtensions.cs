namespace Core.Extension;

public static class TimeSpanExtensions
{
    public static string ToTimeString(this TimeSpan source)
    {
        return source.ToString(@"hh\:mm\:ss");
    }
}
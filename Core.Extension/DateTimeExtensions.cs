namespace Core.Extension;

public static class DateTimeExtensions
{
	public static readonly DateTime UnixTimeZeroPoint = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

	public static string ToyyyyMMdd(this DateTime source)
	{
		return source.ToString("yyyyMMdd");
	}

	public static string ToISO8601String(this DateTime source)
	{
		var iso8601 = source.ToString("o");

		return iso8601.EndsWith("Z", StringComparison.InvariantCultureIgnoreCase)
			? iso8601
			: $"{iso8601}Z";
	}

	public static string ToVietnamString(this DateTime source)
	{
		return source.ToString("dd/MM/yyyy hh:mm:ss");
	}

	public static long ToUnixTime(this DateTime value, bool isInMilliseconds = true)
	{
		var timespan = value.ToUniversalTime().Subtract(UnixTimeZeroPoint);

		return (long)(isInMilliseconds
			? timespan.TotalMilliseconds
			: timespan.TotalSeconds);
	}

	public static bool IsNotNullNorMin(this DateTime value)
	{
		return value != DateTime.MinValue;
	}

	public static bool IsNullOrMin(this DateTime value)
	{
		return value == DateTime.MinValue;
	}

	public static bool IsNotNullNorMin(this DateTime? value)
	{
		return value != null && value != DateTime.MinValue;
	}

	public static bool IsNullOrMin(this DateTime? value)
	{
		return value == null || value == DateTime.MinValue;
	}
}
using Core.Attribute.AutoInjection;
using Core.Helper.Interface;

namespace Core.Helper;

[ScopedAutoInjection]
public class DateTimeHelper : IDateTimeHelper
{
	public DateTime GetNow() => DateTime.Now;

	public DateTime GetUtcNow() => DateTime.UtcNow;

	public DateTimeOffset GetUtcNowOffset() => DateTimeOffset.UtcNow;
}
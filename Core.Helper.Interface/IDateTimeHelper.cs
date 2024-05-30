using Base.Model.Interface;

namespace Core.Helper.Interface;

public interface IDateTimeHelper : IAutoInjection
{
	DateTime GetNow();
	DateTime GetUtcNow();
	DateTimeOffset GetUtcNowOffset();
}
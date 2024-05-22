using Omu.ValueInjecter;

namespace Core.ObjectMapper.Extensions;

public static class ObjectExtensions
{
	public static TV To<T, TV>(this T obj)
	{
		return Mapper.Map<TV>(obj);
	}

	public static TV To<TV>(this object obj)
	{
		return Mapper.Map<TV>(obj);
	}
}
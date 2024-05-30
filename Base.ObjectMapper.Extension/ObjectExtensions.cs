using Omu.ValueInjecter;

namespace Base.ObjectMapper.Extension;

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
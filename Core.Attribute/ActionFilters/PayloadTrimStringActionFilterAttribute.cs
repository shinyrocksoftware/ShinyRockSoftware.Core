using Base.Extension;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core.Attribute.ActionFilters;

public class PayloadTrimStringActionFilterAttribute : ActionFilterAttribute
{
	public override void OnActionExecuting(ActionExecutingContext context)
	{
		var changes = new Dictionary<string, object>();

		foreach ((var key, var value) in context.ActionArguments.Where(a => a.Value != null))
		{
			if (value != null)
			{
				var type = value.GetType();
				if (type.IsEnumerable())
				{
					changes[key] = TrimEnumerable((IEnumerable) value);
				}
				else if (IsComplexObject(type))
				{
					var obj = TrimObject(value);
					if (obj != null)
					{
						changes[key] = obj;
					}
				}
			}
		}

		foreach ((var key, var value) in changes)
		{
			context.ActionArguments[key] = value;
		}
	}

	private static object? TrimObject(object? argValue)
	{
		object? result = null;

		if (argValue != null)
		{
			var argType = argValue.GetType();
			if (argType.IsEnumerable())
			{
				TrimEnumerable((IEnumerable) argValue);
			}

			var props = argType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
			                   .Where(prop => prop.PropertyType == typeof(string)
			                                  && prop.GetIndexParameters().Length == 0
			                                  && prop is { CanWrite: true, CanRead: true });

			foreach (var prop in props)
			{
				var value = prop.GetValue(argValue, null) as string;
				value = value?.Trim();
				prop.SetValue(argValue, value, null);
			}

			result = argValue;
		}

		return result;
	}

	private static IEnumerable TrimEnumerable(IEnumerable value)
	{
		var enumerable = value as object[] ?? value.Cast<object>().ToArray();

		return enumerable.OfType<string>().Any()
			? enumerable.Cast<string>().Select(s => s?.Trim())
			: enumerable.Select(TrimObject);
	}

	private static bool IsComplexObject(Type value) => value is { IsClass: true, IsArray: false };
}
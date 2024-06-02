namespace Base.Extension;

public static class TypeExtensions
{
	public static bool IsEnumerable(this Type source)
	{
		return source.IsAssignableFrom(typeof(IEnumerable));
	}

	public static object? CreateInstance(this Type mappingType, IEnumerable<Assembly> assemblies)
	{
		var constructor = mappingType.GetConstructors()[0];
		var constructorParams = constructor.GetParameters();

		object? mapper;

		if (constructorParams.Length > 0)
		{
			var typeParams = constructorParams.Select(parameterInfo => assemblies.SelectMany(s => s.GetTypes())
			                                                                     .FirstOrDefault(p => p.IsClass && parameterInfo.ParameterType.IsAssignableFrom(p)))
			                                  .ToList();

			var parameters = new object[typeParams.Count];

			for (var i = 0; i < typeParams.Count; i++)
			{
				var underlyingSystemType = typeParams[i]?.UnderlyingSystemType;

				if (underlyingSystemType != null)
				{
					var inst = Activator.CreateInstance(underlyingSystemType);

					if (inst != null)
					{
						parameters[i] = inst;
					}
				}
			}

			mapper = Activator.CreateInstance(mappingType, parameters);
		}
		else
		{
			mapper = Activator.CreateInstance(mappingType);
		}

		return mapper;
	}
}
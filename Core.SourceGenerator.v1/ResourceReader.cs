namespace Core.SourceGenerator.v1;

internal class ResourceReader
{
	public static string GetResource<TAssembly>(string endWith)
	{
		return GetResource(endWith, typeof(TAssembly));
	}

	public static string GetResource(string name, Type? assemblyType = null)
	{
		var assembly = GetAssembly(assemblyType);

		var resourceNames = assembly.GetManifestResourceNames();
		var resources = resourceNames.Where(r => r.EndsWith($".{name}"));

		if (!resources.Any())
			throw new InvalidOperationException($"There is no resources with nane '{name}'");

		var resourceName = resources.Single();

		return ReadEmbeddedResource(assembly, resourceName);
	}

	private static Assembly GetAssembly(Type? assemblyType)
	{
		return assemblyType == null
			? Assembly.GetExecutingAssembly()
			: Assembly.GetAssembly(assemblyType);
	}

	private static string ReadEmbeddedResource(Assembly assembly, string name)
	{
		using var resourceStream = assembly.GetManifestResourceStream(name);
		if (resourceStream == null) return null;
		using var streamReader = new StreamReader(resourceStream);
		return streamReader.ReadToEnd();
	}
}
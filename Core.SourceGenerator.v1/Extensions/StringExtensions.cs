using Pluralize.NET;

namespace Core.SourceGenerator.v1.Extensions;

public static class StringExtensions
{
	public static string Pluralize(this string value)
	{
		return new Pluralizer().Pluralize(value);
	}
}
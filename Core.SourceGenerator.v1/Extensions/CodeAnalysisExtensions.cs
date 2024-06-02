using Core.SourceGenerator.v1.Generators.EntityGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Core.SourceGenerator.v1.Extensions;

public static class CodeAnalysisExtensions
{
	public static IEnumerable<ITypeSymbol> GetBaseTypesAndThis(this ITypeSymbol type)
	{
		var current = type;
		while (current != null)
		{
			yield return current;
			current = current.BaseType;
		}
	}

	public static IEnumerable<ISymbol> GetAllMembers(this ITypeSymbol type)
	{
		return type.GetBaseTypesAndThis().SelectMany(n => n.GetMembers());
	}

	public static CompilationUnitSyntax? GetCompilationUnit(this SyntaxNode syntaxNode)
	{
		return syntaxNode.Ancestors().OfType<CompilationUnitSyntax>().FirstOrDefault();
	}

	public static string GetClassName(this ClassDeclarationSyntax syntax)
	{
		return syntax.Identifier.Text;
	}

	public static string GetClassModifier(this ClassDeclarationSyntax syntax)
	{
		return syntax.Modifiers.ToFullString().Trim();
	}

	public static IEnumerable<EntityPropertyDescriptor?> GetProperties(this ClassDeclarationSyntax syntax, SemanticModel semanticModel)
	{
		return syntax.Members.Select(c => c as PropertyDeclarationSyntax)
		             .Select(propertyDeclarationSyntax =>
		             {
			             if (propertyDeclarationSyntax == null) return null;

			             var type = propertyDeclarationSyntax.Type.ToString();
			             var attributes = propertyDeclarationSyntax.AttributeLists.SelectMany(attributeList => attributeList.Attributes, (_, attributeSyntax) => new EntityAttributeDescriptor
			             {
				             Name = attributeSyntax.Name.ToString()
				             , Values = attributeSyntax.ArgumentList?.Arguments.Select(x => x.Expression.ToFullString()) ?? []
			             });

			             var dbType = type.GetDbType(attributes);

			             return dbType == string.Empty ? null : new EntityPropertyDescriptor
			             {
				             Name = propertyDeclarationSyntax.Identifier.Text
				             , Type = type
				             , NullableType = propertyDeclarationSyntax.Type is NullableTypeSyntax ? type : $"{propertyDeclarationSyntax.Type}?"
				             , DbType = type.GetDbType(attributes)
				             , Required = type.GetRequired(attributes)
				             , Attributes = attributes
			             };
		             })
		             .Where(c => c != null);
	}

	public static bool HaveAttribute(this ClassDeclarationSyntax classSyntax, string attributeName)
	{
		return classSyntax.AttributeLists.Count > 0 &&
		       classSyntax.AttributeLists.SelectMany(al => al.Attributes.Where(a => (a.Name as IdentifierNameSyntax)?.Identifier.Text == attributeName)).Any();
	}

	public static string GetNamespace(this CompilationUnitSyntax? root)
	{
		if (root == null) return string.Empty;

		var namespaceDeclarationSyntax = root.ChildNodes().OfType<NamespaceDeclarationSyntax>();

		if (namespaceDeclarationSyntax.Any())
		{
			return namespaceDeclarationSyntax.First().Name.ToString();
		}

		var fileScopedNamespaceDeclarationSyntax = root.ChildNodes().OfType<FileScopedNamespaceDeclarationSyntax>();

		return fileScopedNamespaceDeclarationSyntax.Any()
			? fileScopedNamespaceDeclarationSyntax.First().Name.ToString()
			: string.Empty;
	}

	public static IEnumerable<string?> GetUsings(this CompilationUnitSyntax root)
	{
		return root.ChildNodes()
		           .OfType<UsingDirectiveSyntax>()
		           .Select(n => n.Name?.ToString());
	}

	private static bool GetRequired(this string dotnetType, IEnumerable<EntityAttributeDescriptor> propertyDescriptorAttributes)
	{
		return dotnetType switch
		{
			"string?" => false
			, _ => propertyDescriptorAttributes.Any(c => c.Name == "Required")
		};
	}

	private static string GetDbType(this string dotnetType, IEnumerable<EntityAttributeDescriptor> propertyDescriptorAttributes)
	{
		switch (dotnetType)
		{
			case "DateTime":
				return "timestamptz(6)";
			case "float":
			case "float?":
				return "float4";
			case "Guid":
			case "Guid?":
				return "uuid";
			case "string":
			case "string?":
				var maxLengthAttribute = propertyDescriptorAttributes.FirstOrDefault(c => c.Name == "MaxLength");
				return maxLengthAttribute == null
					? "text"
					: $"varchar({maxLengthAttribute.Values.First()})";
			default:
				return string.Empty;
		}
	}
}
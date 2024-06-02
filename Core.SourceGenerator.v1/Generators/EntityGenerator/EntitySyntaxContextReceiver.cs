using Core.SourceGenerator.v1.Extensions;
using Core.SourceGenerator.v1.Generators.EntityGenerator.Models;
using Core.SourceGenerator.v1.Generators.MainGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Core.SourceGenerator.v1.Generators.EntityGenerator;

public class EntitySyntaxContextReceiver : ISyntaxContextReceiver
{
	public List<SourceModel> Models { get; set; } = [];

	/// <summary>
	/// Called for every syntax node in the compilation, we can inspect the nodes and save any information useful for generation
	/// </summary>
	public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
	{
		if (context.Node is ClassDeclarationSyntax classSyntax
		    && context.SemanticModel.GetDeclaredSymbol(classSyntax) is ITypeSymbol typeSymbol
		    && typeSymbol.Interfaces.Any(c => c.Name == "IEntity"))
		{
			var model = new EntityModel
			{
				ClientNamespace = typeSymbol.ContainingNamespace.ToDisplayString()
				, Namespace = classSyntax.GetCompilationUnit().GetNamespace()
				, EntityName = classSyntax.GetClassName()
				, EntityNamePlural = classSyntax.GetClassName().Pluralize()
				, ClassModifier = classSyntax.GetClassModifier()
				, Properties = classSyntax.GetProperties(context.SemanticModel).ToList()
			};

			Models.Add(model);
		}
	}
}
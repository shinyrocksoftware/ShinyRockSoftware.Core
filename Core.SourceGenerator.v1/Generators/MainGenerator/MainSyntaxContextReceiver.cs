using Core.SourceGenerator.v1.Extensions;
using Core.SourceGenerator.v1.Generators.MainGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Core.SourceGenerator.v1.Generators.MainGenerator;

public class MainSyntaxContextReceiver : ISyntaxContextReceiver
{
	public List<SourceModel> Models { get; set; } = [];

	/// <summary>
	/// Called for every syntax node in the compilation, we can inspect the nodes and save any information useful for generation
	/// </summary>
	public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
	{
		if (context.Node is ClassDeclarationSyntax classSyntax
		    && context.SemanticModel.GetDeclaredSymbol(classSyntax) is ITypeSymbol typeSymbol)
		{
			var model = new MainModel
			{
				ClientNamespace = typeSymbol.ContainingNamespace.ToDisplayString()
				, Namespace = classSyntax.GetCompilationUnit().GetNamespace()
				, ClassModifier = classSyntax.GetClassModifier()
			};

			Models.Add(model);
		}
	}
}

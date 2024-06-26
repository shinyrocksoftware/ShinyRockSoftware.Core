using Core.SourceGenerator.v1.Abstracts;
using Core.SourceGenerator.v1.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Core.SourceGenerator.v1.Generators.EntityGenerator.Abstracts;

public abstract class BaseEntitySourceGenerator : BaseSourceGenerator
{
	public override ISyntaxContextReceiver GetSyntaxContextReceiverDelegate() => new EntitySyntaxContextReceiver();

	public override void GenerateActionDelegate(IList<GeneratorRegistration> generatorRegistrations, GeneratorExecutionContext context, SourceModel model)
	{
		foreach (var generatorRegistration in generatorRegistrations)
		{
			var result = TemplateRenderer.Execute(generatorRegistration.TemplateResourceFileName, model, TemplateAnchor);
			var generatedFileName = TemplateRenderer.Parse(generatorRegistration.GeneratedFileNameTemplatePatternString, model);
			context.AddSource(generatedFileName, SourceText.From(result, Encoding.UTF8));
		}
	}

	public override void OnExecuting(GeneratorExecutionContext context, ISyntaxContextReceiver receiver)
	{
		if (receiver is EntitySyntaxContextReceiver entitySyntaxContextReceiver)
		{
			var generatorRegistrations = GenerateRegistrations();

			foreach (var model in entitySyntaxContextReceiver.Models)
			{
				GenerateActionDelegate(generatorRegistrations, context, model);
			}
		}
	}
}
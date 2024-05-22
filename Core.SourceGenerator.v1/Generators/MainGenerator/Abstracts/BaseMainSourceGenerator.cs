using Core.SourceGenerator.v1.Abstracts;
using Core.SourceGenerator.v1.Generators.MainGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Core.SourceGenerator.v1.Generators.MainGenerator.Abstracts;

public abstract class BaseMainSourceGenerator : BaseSourceGenerator
{
	public override ISyntaxContextReceiver GetSyntaxContextReceiverDelegate() => new MainSyntaxContextReceiver();

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
		if (receiver is MainSyntaxContextReceiver mainSyntaxContextReceiver)
		{
			var generatorRegistrations = GenerateRegistrations();

			foreach (var model in mainSyntaxContextReceiver.Models)
			{
				GenerateActionDelegate(generatorRegistrations, context, model);
			}
		}
	}
}
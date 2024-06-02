using Core.SourceGenerator.v1.Generators;
using Core.SourceGenerator.v1.Generators.MainGenerator.Models;
using Microsoft.CodeAnalysis;

namespace Core.SourceGenerator.v1.Abstracts;

public abstract class BaseSourceGenerator : ISourceGenerator
{
	public abstract Type TemplateAnchor { get; set; }
	public abstract Func<IList<GeneratorRegistration>> GenerateRegistrations { get; set; }
	public abstract ISyntaxContextReceiver GetSyntaxContextReceiverDelegate();
	public abstract void GenerateActionDelegate(IList<GeneratorRegistration> generatorRegistrations, GeneratorExecutionContext context, SourceModel model);
	public abstract void OnExecuting(GeneratorExecutionContext context, ISyntaxContextReceiver receiver);

	public void Initialize(GeneratorInitializationContext context)
	{
		context.RegisterForSyntaxNotifications(GetSyntaxContextReceiverDelegate);
	}

	public void Execute(GeneratorExecutionContext context)
	{
		if (context.SyntaxContextReceiver is not { } receiver)
			return;

		// if (GenerateRegistrations == null)
		// {
		// 	throw new("GenerateRegistrations must NOT be null");
		// }

		OnExecuting(context, receiver);

		// if (GenerateActionDelegate == null)
		// {
		// 	throw new("GenerateActionDelegate must NOT be null");
		// }
		//
		// foreach (SourceModel model in receiver.Models)
		// {
		// 	GenerateActionDelegate(GenerateRegistrations(), context, model);
		// }
	}
}
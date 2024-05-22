using Core.SourceGenerator.v1.Generators.EntityGenerator.Models;
using Core.SourceGenerator.v1.Generators.MainGenerator.Models;
using Microsoft.CodeAnalysis;

namespace Core.SourceGenerator.v1.Generators.EntityGenerator.Abstracts;

public abstract class BaseLifeCycleEntitySourceGenerator : BaseEntitySourceGenerator
{
	public override void GenerateActionDelegate(IList<GeneratorRegistration> generatorRegistrations, GeneratorExecutionContext context, SourceModel model)
	{
		foreach (var @event in EntityLifeCycleConstants.Names)
		{
			var entityModel = model as EntityModel;
			entityModel.EntityLifeCycleEvent = @event;

			base.GenerateActionDelegate(generatorRegistrations, context, entityModel);
		}
	}
}
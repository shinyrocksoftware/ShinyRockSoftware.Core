using Core.SourceGenerator.v1.Generators;
using Core.SourceGenerator.v1.Generators.EntityGenerator.Abstracts;
using Microsoft.CodeAnalysis;

namespace Core.SourceGenerator.v1.App.Background.EntityGenerators;

[Generator]
public class LifeCycleEntitySourceGenerator : BaseLifeCycleEntitySourceGenerator
{
	public override Type TemplateAnchor { get; set; } = typeof(Anchor);

	public override Func<IList<GeneratorRegistration>> GenerateRegistrations { get; set; } = () => new List<GeneratorRegistration>
	{
		new()
		{
			TemplateResourceFileName = "BackgroundControllerEntityTemplate.scriban"
			, GeneratedFileNameTemplatePatternString = "{{EntityName}}{{EntityLifeCycleEvent}}BackgroundController.g.cs"
		}
		, new()
		{
			TemplateResourceFileName = "BackgroundServiceEntityTemplate.scriban"
			, GeneratedFileNameTemplatePatternString = "{{EntityName}}{{EntityLifeCycleEvent}}BackgroundService.g.cs"
		}
		, new()
		{
			TemplateResourceFileName = "BaseStreamJobEntityTemplate.scriban"
			, GeneratedFileNameTemplatePatternString = "Base{{EntityName}}{{EntityLifeCycleEvent}}StreamJob.g.cs"
		}
		, new()
		{
			TemplateResourceFileName = "IStreamJobEntityTemplate.scriban"
			, GeneratedFileNameTemplatePatternString = "I{{EntityName}}{{EntityLifeCycleEvent}}StreamJob.g.cs"
		}
	};
}
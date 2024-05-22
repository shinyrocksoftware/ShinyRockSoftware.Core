using Core.SourceGenerator.v1.Generators;
using Core.SourceGenerator.v1.Generators.EntityGenerator.Abstracts;
using Microsoft.CodeAnalysis;

namespace Core.SourceGenerator.v1.App.Rest.EntityGenerators;

[Generator]
public class EntitySourceGenerator : BaseEntitySourceGenerator
{
	public override Type TemplateAnchor { get; set; } = typeof(Anchor);

	public override Func<IList<GeneratorRegistration>> GenerateRegistrations { get; set; } = () => new List<GeneratorRegistration>
	{
		new()
		{
			TemplateResourceFileName = "CreateCommandApiRequestEntityTemplate.scriban"
			, GeneratedFileNameTemplatePatternString = "Create{{EntityName}}CommandApiRequest.g.cs"
		},
		new()
		{
			TemplateResourceFileName = "UpdateCommandApiRequestEntityTemplate.scriban"
			, GeneratedFileNameTemplatePatternString = "Update{{EntityName}}CommandApiRequest.g.cs"
		},
		new()
		{
			TemplateResourceFileName = "UpdatePartialCommandApiRequestEntityTemplate.scriban"
			, GeneratedFileNameTemplatePatternString = "Update{{EntityName}}PartialCommandApiRequest.g.cs"
		}
	};
}
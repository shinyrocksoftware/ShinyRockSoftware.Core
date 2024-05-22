using Core.SourceGenerator.v1.Generators;
using Core.SourceGenerator.v1.Generators.EntityGenerator.Abstracts;
using Microsoft.CodeAnalysis;

namespace Core.SourceGenerator.v1.App.DbRequest.EntityGenerators;

[Generator]
public class EntitySourceGenerator : BaseEntitySourceGenerator
{
	public override Type TemplateAnchor { get; set; } = typeof(Anchor);

	public override Func<IList<GeneratorRegistration>> GenerateRegistrations { get; set; } = () => new List<GeneratorRegistration>
	{
		new()
		{
			TemplateResourceFileName = "CreateCommandDbRequestEntityTemplate.scriban"
			, GeneratedFileNameTemplatePatternString = "Create{{EntityName}}CommandDbRequest.g.cs"
		},
		new()
		{
			TemplateResourceFileName = "DeleteCommandDbRequestEntityTemplate.scriban"
			, GeneratedFileNameTemplatePatternString = "Delete{{EntityName}}CommandDbRequest.g.cs"
		},
		new()
		{
			TemplateResourceFileName = "UpdateCommandDbRequestEntityTemplate.scriban"
			, GeneratedFileNameTemplatePatternString = "Update{{EntityName}}CommandDbRequest.g.cs"
		},
		new()
		{
			TemplateResourceFileName = "UpdatePartialCommandDbRequestEntityTemplate.scriban"
			, GeneratedFileNameTemplatePatternString = "Update{{EntityName}}PartialCommandDbRequest.g.cs"
		},
		new()
		{
			TemplateResourceFileName = "GetByIdQueryDbRequestEntityTemplate.scriban"
			, GeneratedFileNameTemplatePatternString = "Get{{EntityName}}ByIdQueryDbRequest.g.cs"
		},
		new()
		{
			TemplateResourceFileName = "GetPagedQueryDbRequestEntityTemplate.scriban"
			, GeneratedFileNameTemplatePatternString = "GetPaged{{EntityName}}QueryDbRequest.g.cs"
		},
	};
}
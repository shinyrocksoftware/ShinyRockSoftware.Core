using Core.SourceGenerator.v1.Generators.MainGenerator.Models;

namespace Core.SourceGenerator.v1.Generators.EntityGenerator.Models;

public class EntityModel : MainModel
{
    public string EntityName { get; set; }
    public string EntityNamePlural { get; set; }
    public string EntityLifeCycleEvent { get; set; }
    public List<EntityPropertyDescriptor?> Properties { get; set; }
}
namespace Core.SourceGenerator.v1.Generators.EntityGenerator.Models;

public class EntityPropertyDescriptor
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string NullableType { get; set; }
    public string DbType { get; set; }
    public bool Required { get; set; }
    public IEnumerable<EntityAttributeDescriptor> Attributes { get; set; }
}
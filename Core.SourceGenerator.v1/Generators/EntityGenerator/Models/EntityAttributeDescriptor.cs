namespace Core.SourceGenerator.v1.Generators.EntityGenerator.Models;

public class EntityAttributeDescriptor
{
    public string Name { get; set; }
    public IEnumerable<string> Values { get; set; }
}
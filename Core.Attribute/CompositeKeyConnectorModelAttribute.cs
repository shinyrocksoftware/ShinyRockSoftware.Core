using System.ComponentModel;

namespace Core.Attribute;

public class CompositeKeyConnectorModelAttribute : DefaultValueAttribute
{
    /// <summary>
    /// Determine if this object key is required or not
    /// </summary>
    public CompositeKeyConnectorModelAttribute() : base(null)
    {
    }
}
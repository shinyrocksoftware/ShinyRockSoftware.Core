namespace Core.Attribute;

[AttributeUsage(AttributeTargets.Class)]
public class OrderAttribute(int order) : System.Attribute
{
	public int Order { get; } = order;

	public OrderAttribute() : this(0)
	{
            
	}
}
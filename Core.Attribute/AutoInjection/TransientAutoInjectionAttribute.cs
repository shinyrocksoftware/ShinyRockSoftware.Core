namespace Core.Attribute.AutoInjection;

public class TransientAutoInjectionAttribute(int order) : OrderAttribute(order)
{
	public TransientAutoInjectionAttribute() : this(0)
	{
	}
}
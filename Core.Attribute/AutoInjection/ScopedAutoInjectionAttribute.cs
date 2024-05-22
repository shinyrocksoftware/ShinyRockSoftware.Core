namespace Core.Attribute.AutoInjection;

public class ScopedAutoInjectionAttribute(int order) : OrderAttribute(order)
{
	public ScopedAutoInjectionAttribute() : this(0)
	{
	}
}
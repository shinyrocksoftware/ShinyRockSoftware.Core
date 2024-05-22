namespace Core.Attribute.AutoInjection;

public class DecoratorAutoInjectionAttribute(int order) : OrderAttribute(order)
{
	public DecoratorAutoInjectionAttribute() : this(0)
	{
	}
}
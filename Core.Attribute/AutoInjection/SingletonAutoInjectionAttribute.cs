namespace Core.Attribute.AutoInjection;

public class SingletonAutoInjectionAttribute(int order) : OrderAttribute(order)
{
    public SingletonAutoInjectionAttribute() : this(0)
    {
    }
}
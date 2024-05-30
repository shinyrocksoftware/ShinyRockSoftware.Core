namespace Base.Extension;

public static class BoolExtensions
{
    public static bool IsTrue(this bool? source)
    {
        return source.HasValue && source.Value;
    }
}
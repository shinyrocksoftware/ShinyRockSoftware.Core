namespace Core.Model;

public static class LockModel
{
    public static readonly object LockObject = new();
    public static IDictionary<string, string> Configurations = new Dictionary<string, string>();
}
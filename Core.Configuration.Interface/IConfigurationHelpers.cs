namespace Core.Configuration.Interface;

public interface IConfigurationHelpers
{
    IDictionary<string, string> GetConfigurations();
}
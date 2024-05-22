namespace Core.Configuration.Interface;

public interface IConfigurationManager
{
	void UpdateConfigurations(IDictionary<string, string> updates);
	void DeleteConfiguration(string key);
	IDictionary<string, string> GetConfigurations();
}
using Core.Model.Interface;

namespace Core.Configuration.Interface;

public interface IServiceConnectorModel : IConnectorModel
{
	public string ClientName { get; set; }
	public string Code { get; set; }
	public string Name { get; set; }
	public string Version { get; set; }
	public string ClientServiceNamePattern { get; }
}
using Base.Model.Interface;

namespace Core.Configuration.Interface;

public interface ILoggerConnectorModel : IConnectorModel
{
	public string SeqHost { get; set; }
	public string OpenSearchHosts { get; set; }
	public string ElasticSearchHosts { get; set; }
	public IEnumerable<string> OpenSearchHostList { get; }
	public IEnumerable<string> ElasticSearchHostList { get; }
}
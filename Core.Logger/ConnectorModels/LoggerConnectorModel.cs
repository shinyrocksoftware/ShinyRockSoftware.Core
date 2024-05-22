using Core.Attribute;
using Core.Configuration.Interface;
using Core.Extension;
using Core.Model.Abstract.ConnectorModels;

namespace Core.Logger.ConnectorModels;

public class LoggerConnectorModel : BaseValidationModel, ILoggerConnectorModel
{
    public string SeqHost { get; set; }
    
    [SingleKeyConnectorModel("logger_opensearch_hosts", required: false)]
    public string OpenSearchHosts { get; set; }
    
    [SingleKeyConnectorModel("logger_elasticsearch_hosts", required: false)]
    public string ElasticSearchHosts { get; set; }

    public IEnumerable<string> OpenSearchHostList =>
        OpenSearchHosts.IsNullOrEmpty()
            ? Enumerable.Empty<string>()
            : OpenSearchHosts.Split(','); 

    public IEnumerable<string> ElasticSearchHostList =>
        ElasticSearchHosts.IsNullOrEmpty()
            ? Enumerable.Empty<string>()
            : ElasticSearchHosts.Split(','); 

    public override bool IsValid => true;
    public override string InvalidMessage => string.Empty;
}
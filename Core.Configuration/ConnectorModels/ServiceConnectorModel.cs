using Core.Configuration.Interface;
using Core.Extension;
using Core.Model.Abstract.ConnectorModels;

namespace Core.Configuration.ConnectorModels;

public class ServiceConnectorModel : BaseValidationModel, IServiceConnectorModel
{
    public string ClientName { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Version { get; set; }

    public string ClientServiceNamePattern => $"{ClientName}-{Code}-{Version}-{{0}}";

    public override bool IsValid => ClientName.IsNotNullNorEmpty()
                                    && Code.IsNotNullNorEmpty()
                                    && Name.IsNotNullNorEmpty()
                                    && Version.IsNotNullNorEmpty();

    public override string InvalidMessage => "Requires 'Service:ClientName', 'Service:Code', 'Service:Name' and 'Service:Version' in appsettings.json";
}
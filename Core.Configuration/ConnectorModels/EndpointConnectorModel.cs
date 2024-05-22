using Core.Attribute;
using Core.Extension;
using Core.Model.Abstract.ConnectorModels;
using Core.Model.Interface;

namespace Core.Configuration.ConnectorModels;

public class EndpointConnectorModel : BaseValidationModel, IConnectorModel
{
    [SingleKeyConnectorModel("endpoint_auth")]
    public string Auth { get; set; }

    public override bool IsValid => Auth.IsNotNullNorEmpty();

    public override string InvalidMessage => "Requires 'endpoint_auth'";
}
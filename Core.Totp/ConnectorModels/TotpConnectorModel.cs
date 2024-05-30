using Base.Extension;
using Core.Attribute;
using Core.Model.Abstract.ConnectorModels;
using Base.Model.Interface;

namespace Core.Totp.ConnectorModels;

public class TotpConnectorModel : BaseValidationModel, IConnectorModel
{
    [SingleKeyConnectorModel("services_totp_secret_key")]
    public string SecretKey { get; set; }

    public override bool IsValid => SecretKey.IsNotNullNorEmpty();

    public override string InvalidMessage => "Requires 'services_totp_secret_key'";
}
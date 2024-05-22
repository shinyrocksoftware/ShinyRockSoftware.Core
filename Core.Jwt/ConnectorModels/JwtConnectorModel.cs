using Core.Attribute;
using Core.Extension;
using Core.Model.Abstract.ConnectorModels;
using Core.Model.Interface;

namespace Core.Jwt.ConnectorModels;

public class JwtConnectorModel : BaseValidationModel, IConnectorModel
{
    [SingleKeyConnectorModel("services_jwt_secret_key")]
    public string SecretKey { get; set; }

    public override bool IsValid => SecretKey.IsNotNullNorEmpty();

    public override string InvalidMessage => "Requires 'services_jwt_secret_key'";
}
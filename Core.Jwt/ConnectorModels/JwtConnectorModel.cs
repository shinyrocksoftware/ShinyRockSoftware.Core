using Base.Extension;
using Core.Attribute;
using Core.Model.Abstract.ConnectorModels;
using Base.Model.Interface;

namespace Core.Jwt.ConnectorModels;

public class JwtConnectorModel : BaseValidationModel, IConnectorModel
{
    [SingleKeyConnectorModel("services_jwt_secret_key")]
    public string SecretKey { get; set; }

    public override bool IsValid => SecretKey.IsNotNullNorEmpty();

    public override string InvalidMessage => "Requires 'services_jwt_secret_key'";
}
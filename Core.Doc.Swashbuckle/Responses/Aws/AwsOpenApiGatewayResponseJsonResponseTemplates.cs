using System.Runtime.Serialization;

namespace Core.Doc.Swashbuckle.Responses.Aws;

public class AwsOpenApiGatewayResponseJsonResponseTemplates
{
    [DataMember(Name ="application/json")]
    public string Content { get; set; }
}
namespace Core.Doc.Swashbuckle.Responses.Aws;

public class AwsOpenApiGatewayResponseJsonResponse
{
    public int StatusCode { get; set; }
    public AwsOpenApiGatewayResponseJsonResponseTemplates ResponseTemplates { get; set; }
}
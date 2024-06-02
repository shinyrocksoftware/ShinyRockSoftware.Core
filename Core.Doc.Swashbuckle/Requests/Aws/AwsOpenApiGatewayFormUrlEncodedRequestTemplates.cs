namespace Core.Doc.Swashbuckle.Requests.Aws;

public class AwsOpenApiGatewayFormUrlEncodedRequestTemplates(string content)
{
    public string ContentType = "application/x-www-form-urlencoded";
    public string Content { get; set; } = content;
}
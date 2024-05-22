namespace Core.Doc.Swashbuckle.Requests.Aws;

public class AwsOpenApiGatewayFormUrlEncodedRequestTemplates
{
    public string ContentType = "application/x-www-form-urlencoded";
    public string Content { get; set; }

    public AwsOpenApiGatewayFormUrlEncodedRequestTemplates(string content)
    {
        Content = content;
    }
}
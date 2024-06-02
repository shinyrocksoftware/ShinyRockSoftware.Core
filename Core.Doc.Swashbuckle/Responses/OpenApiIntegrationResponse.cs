namespace Core.Doc.Swashbuckle.Responses;

public class OpenApiIntegrationResponse(int statusCode, IDictionary<string, string> responseParameters)
{
    public int StatusCode { get; set; } = statusCode;
    public IDictionary<string, string> ResponseParameters { get; } = responseParameters;
}
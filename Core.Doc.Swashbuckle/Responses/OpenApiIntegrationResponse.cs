namespace Core.Doc.Swashbuckle.Responses;

public class OpenApiIntegrationResponse
{
    public int StatusCode { get; set; }
    public IDictionary<string, string> ResponseParameters { get; }

    public OpenApiIntegrationResponse(int statusCode, IDictionary<string, string> responseParameters)
    {
        StatusCode = statusCode;
        ResponseParameters = responseParameters;
    }
}
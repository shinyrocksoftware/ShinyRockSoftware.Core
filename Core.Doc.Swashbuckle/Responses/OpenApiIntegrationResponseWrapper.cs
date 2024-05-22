namespace Core.Doc.Swashbuckle.Responses;

public class OpenApiIntegrationResponseWrapper
{
    //[JsonExtensionData]
    public IDictionary<string, OpenApiIntegrationResponse> Extensions { get; }

    public OpenApiIntegrationResponseWrapper()
    {
        Extensions = new Dictionary<string, OpenApiIntegrationResponse>();
    }
}
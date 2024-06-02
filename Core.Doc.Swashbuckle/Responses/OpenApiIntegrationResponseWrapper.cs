namespace Core.Doc.Swashbuckle.Responses;

public class OpenApiIntegrationResponseWrapper
{
    //[JsonExtensionData]
    public IDictionary<string, OpenApiIntegrationResponse> Extensions { get; } = new Dictionary<string, OpenApiIntegrationResponse>();
}
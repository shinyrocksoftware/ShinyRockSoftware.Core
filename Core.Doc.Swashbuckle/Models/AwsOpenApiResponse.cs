namespace Core.Doc.Swashbuckle.Models;

public class AwsOpenApiResponse
{
    public int StatusCode { get; set; }
    public IDictionary<string, string> ResponseParameters { get; } = new Dictionary<string, string>();
}
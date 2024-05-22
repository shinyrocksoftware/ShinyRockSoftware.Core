using Microsoft.OpenApi.Models;

namespace Core.Doc.Swashbuckle.OperationFilters;

public class OpenApiFormDataParameter : OpenApiParameter
{
    public new string In { get; set; } = "formData";
    public string Type { get; set; }
        
    // [JsonExtensionData]
    // public new Dictionary<string, object> Extensions { get; }

    public OpenApiFormDataParameter(string name, bool required = true, string type = "string", string description = "")
    {
        Name = name;
        Required = required;
        Type = type;
        Description = description;
        //Extensions = new Dictionary<string, object>();
    }
}
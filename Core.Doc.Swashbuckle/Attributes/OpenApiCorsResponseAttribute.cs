using Core.Doc.Swashbuckle.ConnectorModels.Aws;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Core.Doc.Swashbuckle.Attributes;

public class OpenApiCorsResponseAttribute : SwaggerResponseAttribute
{
    public IDictionary<string, OpenApiHeader> Headers { get; }

    public OpenApiCorsResponseAttribute(int statusCode, string? description = null, Type? type = null) : base(statusCode, description, type)
    {
        var header = new OpenApiHeader
        {
            Schema = new()
            {
                Type = "string"
            }
        };

        Headers = new Dictionary<string, OpenApiHeader>
        {
            {
                AwsOpenApiGatewayIntegrationConstants.RESPONSE_HEADER_ACCESS_CONTROL_ALLOW_ORIGIN, header
            }
            ,
            {
                AwsOpenApiGatewayIntegrationConstants.RESPONSE_HEADER_SERVER, header
            }
            ,
            {
                AwsOpenApiGatewayIntegrationConstants.RESPONSE_HEADER_STRICT_TRANSPORT_SECURITY, header
            }
            ,
            {
                AwsOpenApiGatewayIntegrationConstants.RESPONSE_HEADER_X_CONTENT_TYPE_OPTIONS, header
            }
            ,
            {
                AwsOpenApiGatewayIntegrationConstants.RESPONSE_HEADER_X_XSS_PROTECTION, header
            }
            ,
        };
    }
}
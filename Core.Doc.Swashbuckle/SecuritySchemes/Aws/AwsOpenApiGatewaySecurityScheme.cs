using System.Runtime.Serialization;
using Microsoft.OpenApi.Models;

namespace Core.Doc.Swashbuckle.SecuritySchemes.Aws;

public class AwsOpenApiGatewaySecurityScheme : OpenApiSecurityScheme
{
    [DataMember(Name ="x-amazon-apigateway-authtype")]
    public string AmazonApiGatewayAuthType { get; set; }

    [DataMember(Name ="x-amazon-apigateway-authorizer")]
    public AwsOpenApiGatewaySecuritySchemeTokenAuthorizer AmazonApiGatewayAuthorizer { get; set; }

    public AwsOpenApiGatewaySecurityScheme(string region, string accountId, string lambdaFunctionName, int ttlInSeconds = 300,
                                           string name = "Authorization", ParameterLocation @in = ParameterLocation.Header, string description = "description",
                                           string amazonApiGatewayAuthType = "oauth2")
    {
        Name = name;
        Type = SecuritySchemeType.ApiKey;
        In = @in;
        Description = description;
        AmazonApiGatewayAuthType = amazonApiGatewayAuthType;
        AmazonApiGatewayAuthorizer = new(region, accountId, lambdaFunctionName, ttlInSeconds);
    }
}
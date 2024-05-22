namespace Core.Doc.Swashbuckle.SecuritySchemes.Aws;

public class AwsOpenApiGatewaySecuritySchemeTokenAuthorizer
{
    public string Type { get; set; } = "token";
    public string AuthorizerUri { get; set; }
    public int AuthorizerResultTtlInSeconds { get; set; }

    public AwsOpenApiGatewaySecuritySchemeTokenAuthorizer(string region, string accountId, string lambdaFunctionName, int ttlInSeconds = 300)
    {
        AuthorizerUri = $"arn:aws:apigateway:{region}:lambda:path/2015-03-31/functions/arn:aws:lambda:{region}:{accountId}:function:{lambdaFunctionName}/invocations";
        AuthorizerResultTtlInSeconds = ttlInSeconds;
    }
}
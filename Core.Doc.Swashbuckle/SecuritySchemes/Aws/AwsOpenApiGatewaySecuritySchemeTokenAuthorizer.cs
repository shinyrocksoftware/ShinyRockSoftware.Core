namespace Core.Doc.Swashbuckle.SecuritySchemes.Aws;

public class AwsOpenApiGatewaySecuritySchemeTokenAuthorizer(string region, string accountId, string lambdaFunctionName, int ttlInSeconds = 300)
{
    public string Type { get; set; } = "token";
    public string AuthorizerUri { get; set; } = $"arn:aws:apigateway:{region}:lambda:path/2015-03-31/functions/arn:aws:lambda:{region}:{accountId}:function:{lambdaFunctionName}/invocations";
    public int AuthorizerResultTtlInSeconds { get; set; } = ttlInSeconds;
}
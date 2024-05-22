namespace Core.Doc.Swashbuckle.ConnectorModels.Aws;

public class AwsOpenApiGatewayIntegrationConstants
{
    public const string CONTEXT_AUTHORIZER_KEY_ACCESS_TOKEN = "accessToken";
    public const string CONTEXT_AUTHORIZER_KEY_AUTHORIZATION = "authorization";

    public const string TEMPLATE_CONTEXT_AUTHORIZER_0 = "context.authorizer.{0}";
    public const string TEMPLATE_INTEGRATION_REQUEST_0_1 = "integration.request.{0}.{1}";
    public const string TEMPLATE_INTEGRATION_REQUEST_HEADER_0 = "integration.request.header.{0}";
    public const string TEMPLATE_METHOD_REQUEST_0_1 = "method.request.{0}.{1}";
    public const string TEMPLATE_METHOD_RESPONSE_HEADER_0 = "method.response.header.{0}";

    public const string RESPONSE_HEADER_SERVER = "server";
    public const string RESPONSE_HEADER_ACCESS_CONTROL_ALLOW_ORIGIN = "Access-Control-Allow-Origin";
    public const string RESPONSE_HEADER_STRICT_TRANSPORT_SECURITY = "strict-transport-security";
    public const string RESPONSE_HEADER_X_CONTENT_TYPE_OPTIONS = "x-content-type-options";
    public const string RESPONSE_HEADER_X_XSS_PROTECTION = "x-xss-protection";

    public const string RESPONSE_VALUE_STRICT_TRANSPORT_SECURITY = "max-age=315360000; includeSubdomains; preload";
    public const string RESPONSE_VALUE_X_CONTENT_TYPE_OPTIONS = "nosniff";
    public const string RESPONSE_VALUE_X_XSS_PROTECTION = "1; mode=block";

    public const string CONNECTION_TYPE_VPC = "VPC_LINK";

    public const string METHOD_RESPONSE_HEADER_ACCESS_CONTROL_ALLOW_ORIGIN = "method.response.header.Access-Control-Allow-Origin";

    public const string X_AMAZON_API_GATEWAY_INTEGRATION = "x-amazon-apigateway-integration";
}
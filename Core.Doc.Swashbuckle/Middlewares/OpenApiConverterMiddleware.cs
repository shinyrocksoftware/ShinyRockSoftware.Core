using Base.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Readers;

namespace Core.Doc.Swashbuckle.Middlewares;

public class OpenApiConverterMiddleware(RequestDelegate next, string versionId, OpenApiFormat format)
{
    private readonly string _path = $"/swagger/{versionId}/swagger.v3.{(format == OpenApiFormat.Json ? "json" : "yaml")}";
    private readonly ILogger _logger;

    public OpenApiConverterMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, string versionId, OpenApiFormat format) : this(next, versionId, format)
    {
        _logger = loggerFactory.CreateLogger<OpenApiConverterMiddleware>();
    }

    public OpenApiConverterMiddleware(RequestDelegate next, ILogger logger, string versionId, OpenApiFormat format) : this(next, versionId, format)
    {
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var request = context.Request;

        if (request.Path.StartsWithSegments(_path))
        {
            var swaggerUrl = $"{request.Scheme}://{request.Host.Value}/swagger/{versionId}/swagger.json";
            var httpClient = new HttpClient();
            var stream = await httpClient.GetStreamAsync(new Uri(swaggerUrl));
            var openApiDocument = new OpenApiStreamReader().Read(stream, out OpenApiDiagnostic openApiDiagnostic);
            var output = openApiDocument.Serialize(OpenApiSpecVersion.OpenApi3_0, format);

            if (openApiDiagnostic.Errors.IsNotNullNorEmpty())
            {
                foreach (var error in openApiDiagnostic.Errors)
                {
                    _logger.LogError($"Message: {error.Message}, Pointer : {error.Pointer}");
                }
            }

            await context.Response.WriteAsync(output);
        }
        else
        {
            await next.Invoke(context);
        }
    }
}
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Core.Doc.Swashbuckle.DocumentFilters;

public class OpenApiRemoveOpenIdConnectRequestModelDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        swaggerDoc.Components.Schemas.Remove("OpenIdConnectRequest");
        swaggerDoc.Components.Schemas.Remove("ConnectTokenApiRequest");
    }
}
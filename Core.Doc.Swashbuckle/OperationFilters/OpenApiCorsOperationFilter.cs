using Core.Doc.Swashbuckle.Attributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Core.Doc.Swashbuckle.OperationFilters;

public class OpenApiCorsOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        MethodInfo? info = null;
        var methodInfo = context?.ApiDescription?.TryGetMethodInfo(out info);

        if (methodInfo.HasValue && methodInfo.Value && info != null)
        {
            info.GetCustomAttributes<OpenApiCorsResponseAttribute>()
                .ToList()
                .ForEach(attr =>
                {
                    var response = operation.Responses[attr.StatusCode.ToString()];
                    if (response != null)
                    {
                        response.Headers = attr.Headers;
                    }
                });
        }
    }
}
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Core.Doc.Swashbuckle.OperationFilters;

public class OpenApiDeprecatedOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        MethodInfo? info = null;
        var methodInfo = context.ApiDescription?.TryGetMethodInfo(out info);

        if (methodInfo.HasValue && methodInfo.Value && info != null)
        {
            operation.Deprecated = info.GetCustomAttributes<ObsoleteAttribute>(true).Any();
        }
    }
}
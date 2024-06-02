using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Core.Doc.Swashbuckle.OperationFilters;

public class OpenApiHeaderParameterOperationFilter(string headerName) : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation != null)
        {
            var parameter = new OpenApiParameter
            {
                Name = headerName
                , In = ParameterLocation.Header
                , Schema = new()
                {
                    Type = "string"
                }
            };

            if (!operation.Parameters.Contains(parameter))
            {
                operation.Parameters.Add(parameter);
            }
        }
    }
}
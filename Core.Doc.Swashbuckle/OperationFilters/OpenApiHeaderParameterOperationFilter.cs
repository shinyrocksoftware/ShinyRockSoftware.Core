using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Core.Doc.Swashbuckle.OperationFilters;

public class OpenApiHeaderParameterOperationFilter : IOperationFilter
{
    private readonly string _headerName;

    public OpenApiHeaderParameterOperationFilter(string headerName)
    {
        _headerName = headerName;
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation != null)
        {
            var parameter = new OpenApiParameter
            {
                Name = _headerName
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
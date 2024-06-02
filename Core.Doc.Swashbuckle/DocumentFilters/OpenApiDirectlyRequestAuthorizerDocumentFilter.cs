using Base.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Core.Doc.Swashbuckle.DocumentFilters;

public class OpenApiDirectlyRequestAuthorizerDocumentFilter(string authorizerName) : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var path in swaggerDoc.Paths)
        {
            SetSecurity(GetOpenApiOperation(path, OperationType.Get));
            SetSecurity(GetOpenApiOperation(path, OperationType.Post));
            SetSecurity(GetOpenApiOperation(path, OperationType.Put));
            SetSecurity(GetOpenApiOperation(path, OperationType.Patch));
            SetSecurity(GetOpenApiOperation(path, OperationType.Delete));
        }

        foreach (var apiDescription in context.ApiDescriptions)
        {
            if (apiDescription.ActionDescriptor is ControllerActionDescriptor controllerDescriptor)
            {
                var attributes = controllerDescriptor.MethodInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute), true);
                if (attributes.IsNotNullNorEmpty() && controllerDescriptor.AttributeRouteInfo != null)
                {
                    var endpoint = $"/{controllerDescriptor.AttributeRouteInfo.Template}";
                    var actionConstraint = controllerDescriptor.ActionConstraints?.FirstOrDefault();
                        
                    if (actionConstraint is HttpMethodActionConstraint httpMethodConstraint)
                    {
                        var httpMethods = httpMethodConstraint.HttpMethods;
                        foreach (string method in httpMethods)
                        {
                            var path = swaggerDoc.Paths.FirstOrDefault(c => c.Key.EqualsCI(endpoint));

                            if (path.Key.IsNotNullNorEmpty())
                            {
                                switch (method.ToUpper())
                                {
                                    case "GET":
                                        ClearSecurity(GetOpenApiOperation(path, OperationType.Get));
                                        break;
                                    case "POST":
                                        ClearSecurity(GetOpenApiOperation(path, OperationType.Post));
                                        break;
                                    case "PUT":
                                        ClearSecurity(GetOpenApiOperation(path, OperationType.Put));
                                        break;
                                    case "PATCH":
                                        ClearSecurity(GetOpenApiOperation(path, OperationType.Patch));
                                        break;
                                    case "DELETE":
                                        ClearSecurity(GetOpenApiOperation(path, OperationType.Delete));
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private static OpenApiOperation GetOpenApiOperation(KeyValuePair<string, OpenApiPathItem> path, OperationType type)
    {
        var operation = path.Value.Operations.FirstOrDefault(c => c.Key == type);
        return operation.Value;
    }

    private void SetSecurity(OpenApiOperation operation)
    {
        if (operation != null)
        {
            var securityItem = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Name = authorizerName
                    }
                    , new List<string>()
                }
            };

            if (operation.Security != null && operation.Security.Any())
            {
                operation.Security.Add(securityItem);
            }
            else
            {
                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    securityItem
                };
            }
        }
    }

    private void ClearSecurity(OpenApiOperation operation)
    {
        if (operation?.Security != null)
        {
            foreach (var security in operation.Security)
            {
                var schemes = security.FirstOrDefault(c => c.Key.Name == authorizerName);
                if (schemes.Key != null)
                {
                    security.Remove(schemes.Key);
                }
            }

            if (operation.Security.Any() && !operation.Security[0].Any())
            {
                operation.Security = null;
            }
        }
    }
}
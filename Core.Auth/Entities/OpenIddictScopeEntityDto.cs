using Core.Model.Abstract.Entities;
using OpenIddict.EntityFrameworkCore.Models;

namespace Core.Auth.Entities;

public class OpenIddictScopeEntityDto : BasePlainEntityDto<string>
{
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public string Description { get; set; }
    public string Properties { get; set; }
    public string Resources { get; set; }

    public static OpenIddictScopeEntityDto ToDto(OpenIddictEntityFrameworkCoreScope scope)
    {
        return new()
        {
            Id = scope.Id
            , Name = scope.Name
            , DisplayName = scope.DisplayName
            , Description = scope.Description
            , Properties = scope.Properties
            , Resources = scope.Resources
        };
    }
}
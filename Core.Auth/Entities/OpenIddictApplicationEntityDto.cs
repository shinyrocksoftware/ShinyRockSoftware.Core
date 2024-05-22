using Core.Model.Abstract.Entities;
using OpenIddict.EntityFrameworkCore.Models;

namespace Core.Auth.Entities;

public class OpenIddictApplicationEntityDto : BasePlainEntityDto<string>
{
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
    public string? DisplayName { get; set; }
    public string? Permissions { get; set; }
    public string? Properties { get; set; }
    public string? PostLogoutRedirectUris { get; set; }
    public string? RedirectUris { get; set; }
    public string? ApplicationType { get; set; }

    public static OpenIddictApplicationEntityDto ToDto(OpenIddictEntityFrameworkCoreApplication openIddictApplication)
    {
        return new()
        {
            Id = openIddictApplication.Id
            , ClientId = openIddictApplication.ClientId
            , ClientSecret = openIddictApplication.ClientSecret
            , DisplayName = openIddictApplication.DisplayName
            , Permissions = openIddictApplication.Permissions
            , Properties = openIddictApplication.Properties
            , PostLogoutRedirectUris = openIddictApplication.PostLogoutRedirectUris
            , RedirectUris = openIddictApplication.RedirectUris
            , ApplicationType = openIddictApplication.ApplicationType
        };
    }
}
using Core.Extension;
using Microsoft.AspNetCore.Identity;

namespace Core.Auth.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser<Guid>
{
    public override bool EmailConfirmed { get; set; }
    public override bool LockoutEnabled { get; set; }
    public override bool PhoneNumberConfirmed { get; set; }
    public override bool TwoFactorEnabled { get; set; }

    public static ApplicationUser ToUser(string userKey)
    {
        var isPhoneNumber = userKey.IsPhoneNumber(out double phoneNumber);

        return new()
        {
            Email = userKey.IsEmail()
                ? userKey
                : null
            , PhoneNumber = isPhoneNumber
                ? phoneNumber.ToString()
                : null
            , UserName = Guid.NewGuid().ToString().ToUpper()
        };
    }
}
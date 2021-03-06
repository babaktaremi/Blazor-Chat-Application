using System.Security.Claims;
using System.Threading.Tasks;
using ChatApplication.Server.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ChatApplication.Server.IdentityServices
{
    public class AppUserClaimsPrincipleFactory : UserClaimsPrincipalFactory<User, Role>
    {

        public AppUserClaimsPrincipleFactory(UserManager<User> userManager, RoleManager<Role> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var userRoles = await UserManager.GetRolesAsync(user);

            var claimsIdentity = await base.GenerateClaimsAsync(user);
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.Integer));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

            foreach (var roles in userRoles)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, roles));
            }


            return claimsIdentity;
        }
    }
}

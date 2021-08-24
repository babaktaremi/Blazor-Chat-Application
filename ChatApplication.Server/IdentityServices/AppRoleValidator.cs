using System.Threading.Tasks;
using ChatApplication.Server.IdentityModels;
using Microsoft.AspNetCore.Identity;

namespace ChatApplication.Server.IdentityServices
{
    public class AppRoleValidator:RoleValidator<Role>
    {
        public AppRoleValidator(IdentityErrorDescriber errors):base(errors)
        {
            
        }

        public override Task<IdentityResult> ValidateAsync(RoleManager<Role> manager, Role role)
        {
            return base.ValidateAsync(manager, role);
        }
    }
}

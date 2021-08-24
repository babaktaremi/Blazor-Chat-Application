using System.Threading.Tasks;
using ChatApplication.Server.IdentityModels;
using Microsoft.AspNetCore.Identity;

namespace ChatApplication.Server.IdentityServices
{
    public class AppUserValidator:UserValidator<User>
    {
        public AppUserValidator(IdentityErrorDescriber errors):base(errors)
        {
            
        }

        public override async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            var result=await base.ValidateAsync(manager, user);

            return result;
        }
    }
}

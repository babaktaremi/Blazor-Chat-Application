using ChatApplication.Server.Context;
using ChatApplication.Server.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ChatApplication.Server.IdentityServices
{
    public class RoleStore:RoleStore<Role,ApplicationDbContext,int,UserRole,RoleClaim>
    {
        public RoleStore(ApplicationDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}

using System;
using Microsoft.AspNetCore.Identity;

namespace ChatApplication.Server.IdentityModels
{
    public class RoleClaim:IdentityRoleClaim<int>
    {
        public Role Role { get; set; }

    }
  
}

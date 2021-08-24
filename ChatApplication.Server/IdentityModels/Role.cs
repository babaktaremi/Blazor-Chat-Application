using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ChatApplication.Server.IdentityModels
{
    public class Role:IdentityRole<int>
    {
        public ICollection<RoleClaim> Claims { get; set; }
        public ICollection<UserRole> Users { get; set; }


    }
   
}

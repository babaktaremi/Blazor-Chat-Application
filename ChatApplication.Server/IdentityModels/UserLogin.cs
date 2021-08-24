using System;
using Microsoft.AspNetCore.Identity;

namespace ChatApplication.Server.IdentityModels
{
    public class UserLogin:IdentityUserLogin<int>
    {
        public User User { get; set; }
    }

}

using System;
using Microsoft.AspNetCore.Identity;

namespace ChatApplication.Server.IdentityModels
{
    public class UserToken:IdentityUserToken<int>
    {
        public User User { get; set; }
    }
}

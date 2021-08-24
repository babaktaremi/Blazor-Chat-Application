using Microsoft.AspNetCore.Identity;

namespace ChatApplication.Server.IdentityModels
{
    public class UserClaim:IdentityUserClaim<int>
    {
        public User User { get; set; }
    }
  
}

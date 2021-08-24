using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApplication.Shared.Account;
using ChatApplication.Shared.Jwt;

namespace ChatApplication.Client.Service.Login
{
   public interface ILoginService
   {
       Task<AccessToken> GetToken(LoginViewModel model);
   }
}

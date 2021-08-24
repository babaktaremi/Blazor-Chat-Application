using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApplication.Shared.Account;
using ChatApplication.Shared.Jwt;

namespace ChatApplication.Client.Service.Register
{
   public interface IRegisterService
   {
       Task<AccessToken> RegisterUser(RegisterViewModel model);
   }
}

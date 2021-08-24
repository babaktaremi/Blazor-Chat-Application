using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ChatApplication.Server.IdentityModels;
using ChatApplication.Shared.Jwt;

namespace ChatApplication.Server.Service.Jwt
{
   public interface IJwtService
    {
        Task<AccessToken> GenerateAsync(User user);
    }
}

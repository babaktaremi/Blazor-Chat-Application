using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChatApplication.Shared.Jwt
{
   public class AccessToken
    {
        [JsonConstructor]
        public AccessToken()
        {
            
        }

        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public int user_id { get; set; }
        public AccessToken(JwtSecurityToken securityToken,int userId)
        {
            access_token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            token_type = "Bearer";
            expires_in = (int)(securityToken.ValidTo - DateTime.UtcNow).TotalSeconds;
            user_id = userId;
        }
    }
}

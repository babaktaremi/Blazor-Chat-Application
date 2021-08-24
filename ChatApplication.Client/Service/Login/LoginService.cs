using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using ChatApplication.Shared.Account;
using ChatApplication.Shared.Jwt;

namespace ChatApplication.Client.Service.Login
{
    public class LoginService:ILoginService
    {
        private readonly HttpClient _client;

        public LoginService(HttpClient client)
        {
            _client = client;
        }

        public async Task<AccessToken> GetToken(LoginViewModel model)
        {
            var result = await _client.PostAsJsonAsync("Api/Account/Login", model);

            if (!result.IsSuccessStatusCode)
                return null;

            var token = await JsonSerializer.DeserializeAsync<AccessToken>(await result.Content.ReadAsStreamAsync(),new JsonSerializerOptions{ PropertyNameCaseInsensitive = true });

            return token;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using ChatApplication.Shared.Account;
using ChatApplication.Shared.Jwt;

namespace ChatApplication.Client.Service.Register
{
    public class RegisterService:IRegisterService
    {
        private readonly HttpClient _client;

        public RegisterService(HttpClient client)
        {
            _client = client;
        }

        public async Task<AccessToken> RegisterUser(RegisterViewModel model)
        {
            var result = await _client.PostAsJsonAsync("api/account/register", model);

            if (!result.IsSuccessStatusCode)
                return null;

            var token = await JsonSerializer.DeserializeAsync<AccessToken>(await result.Content.ReadAsStreamAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return token;
        }
    }
}

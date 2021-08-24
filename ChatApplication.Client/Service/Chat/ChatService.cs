using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using ChatApplication.Shared.Chat;
using ChatApplication.Shared.Jwt;

namespace ChatApplication.Client.Service.Chat
{
    public class ChatService:IChatService
    {
        private readonly HttpClient _client;

        public ChatService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<UserMessageViewModel>> GetMessages(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            var result = await _client.GetAsync("Api/Chat/Messages");

            var messages =
                await JsonSerializer.DeserializeAsync<List<UserMessageViewModel>>(
                    await result.Content.ReadAsStreamAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return messages;
        }
    }
}

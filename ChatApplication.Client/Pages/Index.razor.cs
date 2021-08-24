using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Blazored.LocalStorage;
using ChatApplication.Client.Service.Chat;
using ChatApplication.Shared.Chat;
using ChatApplication.Shared.Jwt;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Sotsera.Blazor.Toaster;

namespace ChatApplication.Client.Pages
{
    public partial class Index:IAsyncDisposable
    {
        private HubConnection _hub;

        private readonly Timer _debounceTimer = new()
        {
            Interval = 500,
            AutoReset = false
        };

        private string _message;
        private int _userId;
        private List<UserMessageViewModel> _chatHistory=new();
        private string _typingUser;
        

        [Inject] public ILocalStorageService LocalStorageService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public IToaster Toaster { get; set; }
        [Inject] public IChatService ChatService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (!await LocalStorageService.ContainKeyAsync("userToken"))
            {
                NavigationManager.NavigateTo("/Login");
                return;
            }

            _userId = await GetUserIdAsync();

            _chatHistory = await ChatService.GetMessages(await GetAccessTokenValueAsync());
          
            _debounceTimer.Elapsed += async (sender, args) => await IsTyping();

            _hub = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/chat",
                    options => options.AccessTokenProvider = async () => await GetAccessTokenValueAsync())
                .WithAutomaticReconnect()
                .Build();

            _hub.On("UserIsOnline", (UserJoinedChatViewModel user) => Toaster.Info($"{user.UserName} is online"));
            _hub.On("NewMessage", (UserMessageViewModel model) =>UpdateChatHistory(model));
            _hub.On("UserIsTyping",async (UserIsTypingViewModel model) =>await UpdateUserIsTypingAsync(model));

            await _hub.StartAsync();

            Console.WriteLine("Hub Connection Established");
            
        }

        private async Task UpdateUserIsTypingAsync(UserIsTypingViewModel model)
        {
            await InvokeAsync(async () =>
            {
                _typingUser = model.UserName;
                StateHasChanged();

                await Task.Delay(1000);
                _typingUser = string.Empty;
                StateHasChanged();

            });
           
        }

        private void UpdateChatHistory(UserMessageViewModel model)
        {
           _chatHistory.Add(model);
           StateHasChanged();
        }

        async Task<string> GetAccessTokenValueAsync()
        {
            var token = await LocalStorageService.GetItemAsync<AccessToken>("userToken");

            return token.access_token;
        }

        async Task<int> GetUserIdAsync()
        {
            var token = await LocalStorageService.GetItemAsync<AccessToken>("userToken");

            return token.user_id;
        }

        async Task SendMessage()
        {
            if(string.IsNullOrEmpty(_message))
                return;

            await _hub.InvokeAsync("OnNewMessage", new UserNewMessageViewModel {MessageContent = _message});

            _message = string.Empty;

            StateHasChanged();
        }

        void InitiateUserIsTyping()
        {
            _debounceTimer.Stop();
            _debounceTimer.Start();

        }

        async Task IsTyping()
        {
            await _hub.InvokeAsync("OnUserTyping");
            
        }

        public async ValueTask DisposeAsync()
        {
            if (_hub is not null)
                await _hub.DisposeAsync();

            _debounceTimer?.Dispose();
        }
    }
}

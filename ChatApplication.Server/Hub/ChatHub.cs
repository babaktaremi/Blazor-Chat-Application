using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Threading.Tasks;
using ChatApplication.Server.Context;
using ChatApplication.Server.Models;
using ChatApplication.Shared.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatApplication.Server.Hub
{
    [Authorize]
    public class ChatHub:Hub<IChatHub>
    {
        private readonly ApplicationDbContext _db;

        public ChatHub(ApplicationDbContext db)
        {
            _db = db;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Others.UserIsOnline(new UserJoinedChatViewModel {UserName = Context?.User?.Identity?.Name});

            await base.OnConnectedAsync();
        }

        public async Task OnNewMessage(UserNewMessageViewModel model)
        {
            var chat = new ChatHistory
            {
                MessageContent = model.MessageContent,
                UserId = int.Parse(Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            };

            _db.ChatHistories.Add(chat);

            await _db.SaveChangesAsync();

            await Clients.All.NewMessage(new UserMessageViewModel
            {
                MessageContent = chat.MessageContent, MessageDate = chat.Date.ToString("g"),
                UserName = Context?.User?.Identity?.Name,
                UserId = int.Parse(Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            });
        }

        public async Task OnUserTyping()
        {
            var userName = Context?.User?.Identity?.Name;

            await Clients.Others.UserIsTyping(new UserIsTypingViewModel {UserName = userName});
        }
    }
}

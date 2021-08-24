using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ChatApplication.Server.Context;
using ChatApplication.Server.Hub;
using ChatApplication.Server.Models;
using ChatApplication.Shared.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Server.Controllers
{
    [ApiController]
    [Route("Api/Chat")]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ChatController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("Messages")]
        public async Task<IActionResult> GetUserMessages()
        {

            var chats = await _db.ChatHistories.Include(c=>c.User).OrderBy(c=>c.Date).Select(c => new UserMessageViewModel
            {
                UserName = c.User.UserName,
                MessageContent = c.MessageContent,
                MessageDate = c.Date.ToString("g"),
                UserId = c.UserId
            }).ToListAsync();

            return Ok(chats);
        }
    }
}

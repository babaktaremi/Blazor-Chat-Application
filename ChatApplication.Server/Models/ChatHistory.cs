using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApplication.Server.IdentityModels;

namespace ChatApplication.Server.Models
{
    public class ChatHistory
    {
        public ChatHistory()
        {
            Date=DateTime.Now;
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string MessageContent { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}

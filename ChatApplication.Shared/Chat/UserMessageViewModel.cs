using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Shared.Chat
{
   public class UserMessageViewModel
    {
        public string MessageContent { get; set; }
        public string UserName { get; set; }
        public string MessageDate { get; set; }
        public int UserId { get; set; }
    }
}

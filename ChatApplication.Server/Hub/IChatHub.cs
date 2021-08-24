using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApplication.Shared.Chat;

namespace ChatApplication.Server.Hub
{
   public interface IChatHub
   {
       Task UserIsTyping(UserIsTypingViewModel model);
       Task NewMessage(UserMessageViewModel model);
       Task UserIsOnline(UserJoinedChatViewModel model);
   }
}

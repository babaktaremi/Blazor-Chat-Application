using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApplication.Shared.Chat;

namespace ChatApplication.Client.Service.Chat
{
   public interface IChatService
   {
       Task<List<UserMessageViewModel>> GetMessages(string token);
   }
}

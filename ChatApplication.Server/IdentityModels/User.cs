using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApplication.Server.Models;
using Microsoft.AspNetCore.Identity;

namespace ChatApplication.Server.IdentityModels
{
    public class User:IdentityUser<int>
    {
        public ICollection<ChatHistory> ChatHistories { get; set; }
    }
}

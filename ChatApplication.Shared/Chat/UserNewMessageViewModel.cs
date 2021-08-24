using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Shared.Chat
{
   public class UserNewMessageViewModel
    {
        [Required]
        public string MessageContent { get; set; }
    }
}

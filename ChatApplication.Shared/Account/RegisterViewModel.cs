using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Shared.Account
{
   public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [Compare(nameof(RepeatPassword),ErrorMessage = "Password and Repeat Password  Do not Match")]
        public string Password { get; set; }

        [Required]
        public string RepeatPassword { get; set; }
    }
}

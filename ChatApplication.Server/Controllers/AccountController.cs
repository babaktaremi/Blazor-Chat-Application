using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApplication.Server.IdentityModels;
using ChatApplication.Server.IdentityServices;
using ChatApplication.Server.Service.Jwt;
using ChatApplication.Shared.Account;

namespace ChatApplication.Server.Controllers
{
    [ApiController]
    [Route("Api/Account")]
    public class AccountController : ControllerBase
    {
        private readonly AppUserManager _userManager;
        private readonly IJwtService _jwtService;

        public AccountController(AppUserManager userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (await _userManager.FindByNameAsync(model.UserName) != null)
                return BadRequest("Username Already Exists");


            var user = new User {UserName = model.UserName};

            var result = await _userManager.CreateAsync(user,model.Password);

            if (result.Succeeded)
            {
                var token = await _jwtService.GenerateAsync(user);

                return Ok(token);
            }

            var errors = string.Join("|", result.Errors.Select(c => c.Description));

            return BadRequest(errors);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user is null)
                return BadRequest("User Not Found");

            var passwordValidator = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!passwordValidator)
                return BadRequest("Password is not valid");

            var token = await _jwtService.GenerateAsync(user);

            return Ok(token);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using ChatApplication.Client.Service.Login;
using ChatApplication.Shared.Account;
using ChatApplication.Shared.Jwt;
using Microsoft.AspNetCore.Components;
using Sotsera.Blazor.Toaster;

namespace ChatApplication.Client.Pages
{
    public partial class Login
    {
        [Inject] public ILocalStorageService LocalStorageService { get; set; }
        [Inject] public IToaster Toaster { get; set; }
        [Inject] public ILoginService LoginService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        public LoginViewModel LoginViewModel { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            if(await LocalStorageService.ContainKeyAsync("userToken"))
                NavigationManager.NavigateTo("/");

        }

        public async Task HandleValidSubmit()
        {
            var token = await LoginService.GetToken(LoginViewModel);

            if (token is null)
            {
                Toaster.Info("User Not Found");
                return;
            }

            await LocalStorageService.SetItemAsync("userToken", token);
            NavigationManager.NavigateTo("/");
        }
    }
}

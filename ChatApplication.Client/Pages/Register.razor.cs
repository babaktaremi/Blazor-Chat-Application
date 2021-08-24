using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using ChatApplication.Client.Service.Login;
using ChatApplication.Client.Service.Register;
using ChatApplication.Shared.Account;
using Microsoft.AspNetCore.Components;
using Sotsera.Blazor.Toaster;

namespace ChatApplication.Client.Pages
{
    public partial class Register
    {
        [Inject] public ILocalStorageService LocalStorageService { get; set; }
        [Inject] public IToaster Toaster { get; set; }
        [Inject] public IRegisterService RegisterService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        public RegisterViewModel RegisterViewModel { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            if (await LocalStorageService.ContainKeyAsync("userToken"))
                NavigationManager.NavigateTo("/");

        }

        public async Task HandleValidSubmit()
        {
            var token = await RegisterService.RegisterUser(RegisterViewModel);

            if (token is null)
            {
                Toaster.Info("Could not Register User");
                return;
            }

            await LocalStorageService.SetItemAsync("userToken", token);
            NavigationManager.NavigateTo("/");
        }
    }
}

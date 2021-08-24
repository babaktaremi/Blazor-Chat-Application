using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using ChatApplication.Client.Service.Chat;
using ChatApplication.Client.Service.Login;
using ChatApplication.Client.Service.Register;
using Sotsera.Blazor.Toaster.Core.Models;

namespace ChatApplication.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddToaster(options =>
            {
                options.PositionClass = Defaults.Classes.Position.TopCenter;
            });


            builder.Services.AddHttpClient<ILoginService, LoginService>(options =>
                options.BaseAddress = new Uri("http://localhost:5000/"));

            builder.Services.AddHttpClient<IRegisterService, RegisterService>(options =>
                options.BaseAddress = new Uri("http://localhost:5000/"));

            builder.Services.AddHttpClient<IChatService, ChatService>(options =>
                options.BaseAddress = new Uri("http://localhost:5000/"));

            await builder.Build().RunAsync();
        }
    }
}

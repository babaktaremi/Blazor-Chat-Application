using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApplication.Server.IdentityModels;
using ChatApplication.Server.IdentityServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApplication.Server.Extensions
{
    public static class IdentityConfigurationExtension
    {
        public static void AddCustomIdentity(this IServiceCollection services)
        {
            services.AddScoped<IUserValidator<User>, AppUserValidator>();
            services.AddScoped<UserValidator<User>, AppUserValidator>();

            services.AddScoped<IUserClaimsPrincipalFactory<User>, AppUserClaimsPrincipleFactory>();

            services.AddScoped<IRoleValidator<Role>, AppRoleValidator>();
            services.AddScoped<RoleValidator<Role>, AppRoleValidator>();

         
            services.AddScoped<IRoleStore<Role>, RoleStore>();
            services.AddScoped<IUserStore<User>, AppUserStore>();


            services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Stores.ProtectPersonalData = false;

                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                options.Lockout.MaxFailedAccessAttempts = 3;

            }).AddUserStore<AppUserStore>()
                .AddRoleStore<RoleStore>().
                AddUserManager<AppUserManager>().
                AddRoleManager<AppRoleManager>().
                AddDefaultTokenProviders().
            AddSignInManager<AppSignInManager>();

        }
    }
}

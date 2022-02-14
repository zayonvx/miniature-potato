using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OnePlan.Business.Models;
using OnePlan.Data.Context;

namespace OnePlan.Core.Middleware;

public static class AuthorisationMiddleware
{
    public static void SetupAuth(this IServiceCollection services)
    {
        ServiceProvider serviceProvider = services.BuildServiceProvider();
        services.AddIdenityAndConfigure();
    }

    private static void AddIdenityAndConfigure(this IServiceCollection services)
    {
        services.Configure<IdentityOptions>(opt =>
        {
            opt.Password.RequiredLength = 10;
            opt.Password.RequireDigit = true;
            opt.Password.RequireLowercase = true;
            opt.Password.RequireUppercase = true;
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
            opt.Lockout.MaxFailedAccessAttempts = 3;
            opt.User.RequireUniqueEmail = true;
        });

        services.AddIdentity<User, Role>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
    }
}

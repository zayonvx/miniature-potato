using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OnePlan.Business.Models;
using OnePlan.Business.Settings;
using OnePlan.Data.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OnePlan.Business.Consts;

namespace OnePlan.Core.Middleware;

public static class AuthorisationMiddleware
{
    public static void SetupAuth(this IServiceCollection services)
    {
        ServiceProvider serviceProvider = services.BuildServiceProvider();
        var jwtOptions = serviceProvider.GetService<IOptions<JwtSettings>>();
        services.AddIdenityAndConfigure();
        ConfigureJwt(services, jwtOptions.Value);
        services.AddAuthorization();
        services.AddCors(opt =>
        {
            opt.AddDefaultPolicy(builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
            );
        });
    }

    private static void ConfigureJwt(this IServiceCollection services, JwtSettings jwtSettings)
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = false;
            cfg.TokenValidationParameters = new TokenValidationParameters()
            {
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ValidateIssuer = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtSettings.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(jwtSettings.Secret)),
                RoleClaimType = CustomClaims.Role,
                ClockSkew = TimeSpan.Zero
            };
        });
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

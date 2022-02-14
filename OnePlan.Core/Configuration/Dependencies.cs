using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnePlan.Business.Models;
using OnePlan.Data.Context;

namespace OnePlan.Data.Dependencies;

public static class Dependencies
{
    public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                npgSqlOptions => npgSqlOptions.EnableRetryOnFailure()),
            ServiceLifetime.Transient
        );

        services.RegisterServices();
    }

    private static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<UserManager<User>>();
    } 
}
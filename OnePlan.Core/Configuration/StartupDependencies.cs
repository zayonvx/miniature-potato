using Microsoft.EntityFrameworkCore;
using OnePlan.Data.Context;

namespace OnePlan.Core.Dependencies;

public static class StartupDependencies
{
    public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterDependencies(configuration);
    }

    public static void RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        );
    }
}

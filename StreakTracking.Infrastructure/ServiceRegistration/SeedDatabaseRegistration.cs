using Microsoft.Extensions.DependencyInjection;
using StreakTracking.Application.Helpers;
using StreakTracking.Infrastructure.Helpers;

namespace StreakTracking.Infrastructure.ServiceRegistration;

public static class SeedDatabaseRegistration
{
    public static IServiceCollection AddDatabaseSeedingService(this IServiceCollection services)
    {
        services.AddTransient<ISeedDatabase,SeedDatabase>();
        return services;
    }
}
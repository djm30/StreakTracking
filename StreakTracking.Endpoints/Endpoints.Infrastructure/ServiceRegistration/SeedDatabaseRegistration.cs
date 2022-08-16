using Microsoft.Extensions.DependencyInjection;
using StreakTracking.Endpoints.Application.Helpers;
using StreakTracking.Endpoints.Infrastructure.Helpers;

namespace StreakTracking.Endpoints.Infrastructure.ServiceRegistration;

public static class SeedDatabaseRegistration
{
    public static IServiceCollection AddDatabaseSeedingService(this IServiceCollection services)
    {
        services.AddSingleton<ISeedDatabase, SeedDatabase>();
        return services;
    }
}
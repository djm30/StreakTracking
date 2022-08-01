using StreakTracking.Infrastructure.Repositories;
using StreakTracking.Infrastructure.Services;

namespace StreakTracking.API.Extensions;

public static class InfrastructureServiceRegistrations
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IStreakReadRepository, StreakReadRepository>();
        services.AddSingleton<ISqlConnectionService,SqlConnectionService>();
        return services;
    }
}
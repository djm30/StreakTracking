using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StreakTracking.Infrastructure.Repositories;
using StreakTracking.Infrastructure.Services;

namespace StreakTracking.EventHandler.Extensions;

public static class InfrastructureServiceRegistrations
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<ISqlConnectionService,SqlConnectionService>();
        services.AddTransient<IStreakWriteRepository, StreakWriteRepository>();
        services.AddTransient<IStreakDayWriteRepository, StreakDayWriteRepository>();
        return services;
    }
}
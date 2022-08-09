using Microsoft.Extensions.DependencyInjection;
using StreakTracking.Application.Contracts.Persistance;
using StreakTracking.Infrastructure.Repositories;
using StreakTracking.Infrastructure.Services;

namespace StreakTracking.Infrastructure.ServiceRegistration;

public static class EventHandlerServiceRegistration
{
    public static IServiceCollection AddEventHandlerServices(this IServiceCollection services)
    {
        services.AddTransient<ISqlConnectionService,SqlConnectionService>();
        services.AddTransient<IStreakWriteRepository, StreakWriteRepository>();
        services.AddTransient<IStreakDayWriteRepository, StreakDayWriteRepository>();
        return services;
    }
}
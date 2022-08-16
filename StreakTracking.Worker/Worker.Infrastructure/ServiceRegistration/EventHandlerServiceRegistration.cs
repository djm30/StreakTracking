using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using StreakTracking.Common.Contracts;
using StreakTracking.Common.Services;
using StreakTracking.Worker.Application.Contracts.Persistence;
using StreakTracking.Worker.Infrastructure.Repositories;

namespace StreakTracking.Worker.Infrastructure.ServiceRegistration;

public static class EventHandlerServiceRegistration
{
    public static IServiceCollection AddEventHandlerServices(this IServiceCollection services)
    {
        services.AddTransient<ISqlConnectionService<NpgsqlConnection>,SqlConnectionService>();
        services.AddTransient<IStreakWriteRepository, StreakWriteRepository>();
        services.AddTransient<IStreakDayWriteRepository, StreakDayWriteRepository>();
        return services;
    }
}
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using StreakTracking.Application.Contracts.Persistance;
using StreakTracking.Infrastructure.Repositories;
using StreakTracking.Infrastructure.Services;

namespace StreakTracking.Infrastructure.ServiceRegistration;

public static class ApiServiceRegistration
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IStreakReadRepository, StreakReadRepository>();
        services.AddSingleton<ISqlConnectionService<NpgsqlConnection>,SqlConnectionService>();
        return services;
    }
}
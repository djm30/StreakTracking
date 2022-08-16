using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using StreakTracking.Common.Contracts;
using StreakTracking.Common.Services;
using StreakTracking.Endpoints.Application.Contracts.Persistence;
using StreakTracking.Endpoints.Infrastructure.Repositories;

namespace StreakTracking.Endpoints.Infrastructure.ServiceRegistration;

public static class ApiServiceRegistration
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IStreakReadRepository, StreakReadRepository>();
        services.AddSingleton<ISqlConnectionService<NpgsqlConnection>,SqlConnectionService>();
        return services;
    }
}
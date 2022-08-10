using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StreakTracking.Infrastructure.Repositories;
using StreakTracking.Infrastructure.ServiceRegistration;
using StreakTracking.Infrastructure.Services;

namespace StreakTracking.EventHandler.Extensions;

public static class InfrastructureServiceRegistrations
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddEventHandlerServices();
        return services;
    }
}
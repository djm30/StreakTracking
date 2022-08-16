using Microsoft.Extensions.DependencyInjection;
using StreakTracking.Worker.Infrastructure.ServiceRegistration;

namespace StreakTracking.Worker.EventHandler.Extensions;

public static class InfrastructureServiceRegistrations
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddEventHandlerServices();
        return services;
    }
}
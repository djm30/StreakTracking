using StreakTracking.Infrastructure.ServiceRegistration;

namespace StreakTracking.API.Extensions;

public static class InfrastructureServiceRegistrations
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddApiServices();
        return services;
    }
}
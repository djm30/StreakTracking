using StreakTracking.Endpoints.Infrastructure.ServiceRegistration;

namespace StreakTracking.Endpoints.API.Extensions;

public static class InfrastructureServiceRegistrations
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddApiServices();
        return services;
    }
}
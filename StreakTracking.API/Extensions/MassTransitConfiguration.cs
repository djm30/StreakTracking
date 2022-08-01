using MassTransit;

namespace StreakTracking.API.Extensions;

public static class MassTransitConfiguration
{
    public static IServiceCollection ConfigureMassTransit(this IServiceCollection services)
    {
        services.AddMassTransit(x => { x.UsingRabbitMq(); });
        services.AddMassTransitHostedService();
        return services;
    }
}
using MassTransit;

namespace StreakTracking.Endpoints.API.Extensions;

public static class MassTransitConfiguration
{
    public static IServiceCollection ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(config =>
        {
            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(configuration["EventBusConnection"]);
            });
        });
        services.AddMassTransitHostedService();
        return services;
    }
}
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StreakTracking.Worker.EventHandler.Consumers;

namespace StreakTracking.Worker.EventHandler.Extensions;

public static class MassTransitConfiguration
{
    public static IServiceCollection ConfigureMassTransitConsumers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(config =>
        {
            config.AddConsumer<AddStreakConsumer>();
            config.AddConsumer<UpdateStreakConsumer>();
            config.AddConsumer<DeleteStreakConsumer>();
            config.AddConsumer<StreakCompleteConsumer>();
            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(configuration["EventBusConnection"]);
                cfg.ReceiveEndpoint("streaks-queue", c =>
                {
                    c.ConfigureConsumer<AddStreakConsumer>(ctx);
                    c.ConfigureConsumer<UpdateStreakConsumer>(ctx);
                    c.ConfigureConsumer<DeleteStreakConsumer>(ctx);
                    c.ConfigureConsumer<StreakCompleteConsumer>(ctx);
                });
            });
        });
        services.AddMassTransitHostedService();
        return services;
    }
}
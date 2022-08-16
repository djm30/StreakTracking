using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using StreakTracking.Worker.EventHandler.Consumers;

namespace StreakTracking.Worker.EventHandler.Extensions;

public static class MassTransitConfiguration
{
    public static IServiceCollection ConfigureMassTransitConsumers(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<AddStreakConsumer>();
            x.AddConsumer<UpdateStreakConsumer>();
            x.AddConsumer<DeleteStreakConsumer>();
            x.AddConsumer<StreakCompleteConsumer>();
            x.UsingRabbitMq((ctx, cfg) =>
            {
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
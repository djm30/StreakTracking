using MassTransit;

namespace StreakTracking.API.Extensions;

public static class MassTransitConfiguration
{
    public static WebApplicationBuilder ConfigureMassTransit(this WebApplicationBuilder builder)
    {
        builder.Services.AddMassTransit(x => { x.UsingRabbitMq(); });
        builder.Services.AddMassTransitHostedService();
        return builder;
    }
}
using StreakTracking.API.Services;

namespace StreakTracking.API.Extensions;

public static class ServiceRegistration
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddScoped<IStreakReadingService, StreakReadingService>();
        builder.Services.AddScoped<IEventPublishingService, EventPublishingService>();
        return builder;
    }
}
using StreakTracking.Application;
using StreakTracking.Application.Contracts.Business;
using StreakTracking.Application.Services;

namespace StreakTracking.API.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IStreakReadingService, StreakReadingService>();
        services.AddScoped<IEventPublishingService, EventPublishingService>();
        return services;
    }
}
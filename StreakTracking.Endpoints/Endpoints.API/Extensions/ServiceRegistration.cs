using StreakTracking.Endpoints.Application.Contracts.Business;
using StreakTracking.Endpoints.Application.Services;

namespace StreakTracking.Endpoints.API.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IStreakReadingService, StreakReadingService>();
        services.AddScoped<IEventPublishingService, EventPublishingService>();
        return services;
    }
}
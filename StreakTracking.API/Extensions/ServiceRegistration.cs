using StreakTracking.Application.Contracts.Business;
using StreakTracking.Application.Services;

namespace StreakTracking.API.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddScoped<IStreakReadingService, StreakReadingService>();
        services.AddScoped<IEventPublishingService, EventPublishingService>();
        return services;
    }
}
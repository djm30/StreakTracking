using Microsoft.Extensions.DependencyInjection;
using StreakTracking.EventHandler.Services;


namespace StreakTracking.EventHandler.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IStreakWriteService, StreakWriteService>();
        return services;
    }
}
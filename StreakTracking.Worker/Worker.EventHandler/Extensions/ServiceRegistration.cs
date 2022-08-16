using Microsoft.Extensions.DependencyInjection;
using StreakTracking.Worker.Application.Contracts.Business;
using StreakTracking.Worker.Application.Services;

namespace StreakTracking.Worker.EventHandler.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IStreakWriteService, StreakWriteService>();
        return services;
    }
}
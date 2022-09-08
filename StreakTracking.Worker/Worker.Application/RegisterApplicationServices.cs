using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StreakTracking.Worker.Application.Contracts.Business;
using StreakTracking.Worker.Application.Services;

namespace StreakTracking.Worker.Application;

public static class RegisterApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddSingleton<INotificationPublisher, NotificationPublisher>();
        
        return services;
    }
}
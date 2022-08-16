using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace StreakTracking.Endpoints.Application;

public static class RegisterApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        
        return services;
    }
}
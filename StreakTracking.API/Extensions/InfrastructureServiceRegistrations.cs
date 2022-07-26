using StreakTracking.Infrastructure.Repositories;
using StreakTracking.Infrastructure.Services;

namespace StreakTracking.API.Extensions;

public static class InfrastructureServiceRegistrations
{
    public static WebApplicationBuilder AddInfrastructureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IStreakReadRepository, StreakReadRepository>();
        builder.Services.AddSingleton<ISqlConnectionService,SqlConnectionService>();
        return builder;
    }
}
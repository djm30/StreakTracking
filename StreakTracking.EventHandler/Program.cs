using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using StreakTracking.EventHandler.Consumers;
using StreakTracking.EventHandler.Extensions;
using StreakTracking.Services;

namespace StreakTracking.EventHandler
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<AddStreakConsumer>();
                        x.AddConsumer<UpdateStreakConsumer>();
                        x.AddConsumer<DeleteStreakConsumer>();
                        x.AddConsumer<StreakCompleteConsumer>();
                        x.UsingRabbitMq((ctx, cfg) =>
                        {
                            
                            cfg.ReceiveEndpoint("streaks-queue", c =>
                            {
                                c.ConfigureConsumer<AddStreakConsumer>(ctx);
                                c.ConfigureConsumer<UpdateStreakConsumer>(ctx);
                                c.ConfigureConsumer<DeleteStreakConsumer>(ctx);
                                c.ConfigureConsumer<StreakCompleteConsumer>(ctx);
                            });
    
                            
                        });
                    });
                    services.AddScoped<IStreakRemovalService, StreakRemovalService>();
                    services.AddInfrastructureServices();
                    // services.AddHostedService<EventHandlingService>();
                });
    }
}

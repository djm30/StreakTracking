using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using StreakTracking.EventHandler.Consumers;

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
                        x.UsingRabbitMq((ctx, cfg) =>
                        {
                            
                            cfg.ReceiveEndpoint("streaks-queue", c =>
                            {
                                c.ConfigureConsumer<AddStreakConsumer>(ctx);
                            });
    
                            
                        });
                    });
                    services.AddTransient<IStreakWriteRepository, StreakWriteRepository>();
                    // services.AddHostedService<EventHandlingService>();
                });
    }
}

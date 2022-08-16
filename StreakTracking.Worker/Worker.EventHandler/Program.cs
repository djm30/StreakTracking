using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using StreakTracking.Worker.EventHandler.Extensions;

namespace StreakTracking.Worker.EventHandler
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
                    // Extension methods to register all required services
                    services.AddServices();
                    services.AddInfrastructureServices();
                    services.ConfigureMassTransitConsumers();
                    Console.WriteLine("hello?");
                });
    }
}

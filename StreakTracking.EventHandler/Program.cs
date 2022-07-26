using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using StreakTracking.EventHandler.Extensions;

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
                    // Extension methods to register all required services
                    services.ConfigureMassTransit();
                    services.AddServices();
                    services.AddInfrastructureServices();
                });
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotNetAgent.CLI
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    DumpConfig(hostContext.Configuration);
                    var agentConfigSection = hostContext.Configuration.GetSection(nameof(AgentConfig));
                    var agentConfig = agentConfigSection.Get<AgentConfig>();
                    services.AddSingleton(agentConfig);
                    services.AddSingleton<IHostedService, ExampleTimedHostedService>();
                    services.AddHostedService<ExampleBackgroundService>();
                });
            await hostBuilder.RunConsoleAsync();
        }
        public static void DumpConfig(IConfiguration config){
            var keyValuePairs = config.AsEnumerable().ToList();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==============================================");
            Console.WriteLine("Configurations...");
            Console.WriteLine("==============================================");
            foreach (var pair in keyValuePairs)
            {
                Console.WriteLine($"{pair.Key} - {pair.Value}");
            }
        }
    }

    public class AgentConfig {
        public int MaxParallelRequests { get; set; }
    }
}

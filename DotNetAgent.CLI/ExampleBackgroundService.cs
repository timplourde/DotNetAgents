using System;
using System.Threading;
using System.Threading.Tasks;
using DotNetAgent.CLI;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class ExampleBackgroundService : BackgroundService
{
    private readonly ILogger _logger;

    private AgentConfig _config;

    private int _counter;
    public ExampleBackgroundService(ILogger<ExampleBackgroundService> logger, AgentConfig config)
    {
        _logger = logger;
        _config = config;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
         _logger.LogInformation($"ExampleBackgroundService is starting.");

        stoppingToken.Register(() =>
            _logger.LogInformation($"ExampleBackgroundService stoppingToken is stopping."));

        while (!stoppingToken.IsCancellationRequested)
        {
            try{       
                await FakeLongRunningTask(stoppingToken);
            }
            catch(OperationCanceledException){
                _logger.LogInformation($"OP CANCELLED");
            }
        }
        _logger.LogInformation($"GracePeriod background task is stopping.");
    }

    private async Task FakeLongRunningTask(CancellationToken cancellationToken){
        _logger.LogInformation($"ExampleBackgroundService task doing background work : {_counter++}");
        cancellationToken.ThrowIfCancellationRequested();
        await Task.Delay(3_000, cancellationToken);
    }
}
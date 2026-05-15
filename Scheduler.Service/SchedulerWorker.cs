using Microsoft.Extensions.Hosting;
using Scheduler.Service.Core;

namespace Scheduler.Service;

public class SchedulerWorker : BackgroundService
{
    private readonly SchedulerCore _core = new();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _core.RunDueTasks();
            await Task.Delay(5000, stoppingToken); // ogni 5 secondi
        }
    }
}
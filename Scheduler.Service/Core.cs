using MongoDB.Driver;
using Scheduler.Service.Models;
using Cronos;
using System.Net.Http.Json;

namespace Scheduler.Service.Core;

public class SchedulerCore
{
    private readonly IMongoCollection<ScheduledTask> _tasks;
    private readonly HttpClient _http = new();

    public SchedulerCore()
    {
        var client = new MongoClient("mongodb://mongo:27017");
        var db = client.GetDatabase("bos-scheduler");
        _tasks = db.GetCollection<ScheduledTask>("tasks");
    }

    public async Task<List<ScheduledTask>> GetTasks() =>
        await _tasks.Find(_ => true).ToListAsync();

    public async Task<ScheduledTask> AddTask(ScheduledTask task)
    {
        await _tasks.InsertOneAsync(task);
        return task;
    }

    public async Task RunDueTasks()
    {
        var tasks = await GetTasks();
        var now = DateTime.UtcNow;

        foreach (var task in tasks.Where(t => t.Active))
        {
            var cron = CronExpression.Parse(task.Cron);
            var next = cron.GetNextOccurrence(task.LastRun ?? now.AddMinutes(-10), TimeZoneInfo.Utc);

            if (next.HasValue && next.Value <= now)
            {
                await ExecuteTask(task);
            }
        }
    }

    private async Task ExecuteTask(ScheduledTask task)
    {
        try
        {
            await _http.GetAsync(task.TargetUrl);
            task.LastRun = DateTime.UtcNow;
            await _tasks.ReplaceOneAsync(t => t.Id == task.Id, task);

            await SendAudit(task, "success");
        }
        catch
        {
            await SendAudit(task, "failed");
            await SendNotification(task);
        }
    }

    private async Task SendAudit(ScheduledTask task, string result)
    {
        var entry = new
        {
            Service = "scheduler",
            Action = $"task.{result}",
            User = "system",
            Result = result,
            Data = task
        };

        try
        {
            await _http.PostAsJsonAsync("http://audit:8080/audit/log", entry);
        }
        catch { }
    }

    private async Task SendNotification(ScheduledTask task)
    {
        var evt = new
        {
            Type = "scheduler.error",
            Payload = task
        };

        try
        {
            await _http.PostAsJsonAsync("http://notificationcenter:8080/notifications/publish", evt);
        }
        catch { }
    }
}
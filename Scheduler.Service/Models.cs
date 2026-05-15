namespace Scheduler.Service.Models;

public class ScheduledTask
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = "";
    public string Cron { get; set; } = "";
    public string TargetUrl { get; set; } = "";
    public bool Active { get; set; } = true;
    public DateTime? LastRun { get; set; }
}
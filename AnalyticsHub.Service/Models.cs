namespace AnalyticsHub.Service.Models;

public class MetricSnapshot
{
    public string Service { get; set; } = "";
    public double Cpu { get; set; }
    public double Memory { get; set; }
    public double Uptime { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
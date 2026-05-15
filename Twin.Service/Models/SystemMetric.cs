namespace Twin.Service.Models;

public class SystemMetric
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string NodeId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public double Value { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
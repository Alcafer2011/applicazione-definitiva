namespace NotificationCenter.Service.Models;

public class NotificationEvent
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Type { get; set; } = "";
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public Dictionary<string, object> Payload { get; set; } = new();
}

public class WebhookTarget
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Url { get; set; } = "";
    public List<string> EventTypes { get; set; } = new();
}
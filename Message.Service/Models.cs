namespace Message.Service.Models;

public class BusMessage
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Topic { get; set; } = "";
    public Dictionary<string, object> Payload { get; set; } = new();
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
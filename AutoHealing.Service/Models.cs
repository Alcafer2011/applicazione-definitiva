namespace AutoHealing.Service.Models;

public class ServiceStatus
{
    public string Name { get; set; } = "";
    public string State { get; set; } = "";
    public DateTime LastCheck { get; set; }
}
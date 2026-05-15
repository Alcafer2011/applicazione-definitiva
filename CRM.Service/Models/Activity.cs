namespace CRM.Service.Models;

public class Activity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CustomerId { get; set; } = default!;
    public string Type { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime Date { get; set; } = DateTime.UtcNow;
}
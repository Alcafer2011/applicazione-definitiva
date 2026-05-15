namespace CRM.Service.Models;

public class Contact
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CustomerId { get; set; } = default!;
    public string Note { get; set; } = default!;
    public DateTime Date { get; set; } = DateTime.UtcNow;
}
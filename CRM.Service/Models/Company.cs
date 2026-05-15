namespace CRM.Service.Models;

public class Company
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = default!;
    public string VatNumber { get; set; } = default!;
    public string Address { get; set; } = default!;
}
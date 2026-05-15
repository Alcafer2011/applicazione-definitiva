namespace AIEnterpriseOS.Finance.Service.Models;

public class DueDate
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public bool IsPaid { get; set; }
}

public class CostCenter
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
}

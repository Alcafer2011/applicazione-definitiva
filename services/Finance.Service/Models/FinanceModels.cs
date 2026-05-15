namespace AIEnterpriseOS.Finance.Service.Models;

public enum InvoiceType
{
    Sales,
    Purchase
}

public enum InvoiceStatus
{
    Draft,
    Sent,
    Paid,
    Overdue
}

public class InvoiceLine
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Description { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total => Quantity * UnitPrice;
}

public class Invoice
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public InvoiceType Type { get; set; }
    public string CustomerId { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; } = DateTime.UtcNow;
    public DateTime DueDate { get; set; }
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;
    public List<InvoiceLine> Lines { get; set; } = new();
    public decimal Total => Lines.Sum(x => x.Total);
public List<DueDate> DueDates { get; set; } = new();
public string? CostCenterId { get; set; }
public decimal PaidAmount { get; set; }
public decimal RemainingAmount => Total - PaidAmount;
}

public class Payment
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string InvoiceId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime PaidAt { get; set; } = DateTime.UtcNow;
public bool IsPartial { get; set; }
}

public class BankStatementRecord
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}

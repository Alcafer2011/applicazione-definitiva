namespace AIEnterpriseOS.DocumentAI.Service.Models;

public enum DocumentType
{
    Unknown,
    Invoice,
    Order,
    Contract,
    DeliveryNote,
    Receipt
}

public class DocumentAnalysis
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DocumentType Type { get; set; }
    public string RawText { get; set; } = string.Empty;
    public Dictionary<string, string> Fields { get; set; } = new();
    public DateTime ProcessedAt { get; set; } = DateTime.UtcNow;
}

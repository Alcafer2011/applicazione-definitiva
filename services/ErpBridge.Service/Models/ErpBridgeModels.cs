namespace AIEnterpriseOS.ErpBridge.Service.Models;

public class ErpSystemConfig
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string Type { get; set; } = ""; // es: "Zucchetti", "TeamSystem", "SAP", "Custom"
    public string ApiBaseUrl { get; set; } = "";
    public string ApiKey { get; set; } = "";
}

public class ErpInvoiceImportRequest
{
    public string ErpConfigId { get; set; } = "";
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}

public class ErpInvoiceSummary
{
    public string InvoiceNumber { get; set; } = "";
    public DateTime InvoiceDate { get; set; }
    public string CustomerName { get; set; } = "";
    public decimal TotalAmount { get; set; }
    public string Currency { get; set; } = "EUR";
}

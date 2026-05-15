namespace AIEnterpriseOS.OcrDdt.Service.Models;

public class OcrDdtRequest
{
    public string FileName { get; set; } = "";
    public byte[] FileContent { get; set; } = System.Array.Empty<byte>();
}

public class OcrDdtResult
{
    public string DocumentNumber { get; set; } = "";
    public DateTime DocumentDate { get; set; }
    public string SupplierName { get; set; } = "";
    public List<OcrDdtLine> Lines { get; set; } = new();
}

public class OcrDdtLine
{
    public string SkuOrCode { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal Quantity { get; set; }
    public string UnitOfMeasure { get; set; } = "PZ";
}

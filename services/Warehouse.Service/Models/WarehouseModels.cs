namespace AIEnterpriseOS.Warehouse.Service.Models;

public class WarehouseItem
{
    public string Id { get; set; } = "";
    public string Sku { get; set; } = "";
    public string Description { get; set; } = "";
    public string UnitOfMeasure { get; set; } = "PZ";
}

public class WarehouseStock
{
    public string ItemId { get; set; } = "";
    public string Location { get; set; } = "MAIN";
    public decimal Quantity { get; set; }
}

public class WarehouseMovement
{
    public string Id { get; set; } = "";
    public string ItemId { get; set; } = "";
    public DateTime Date { get; set; }
    public string Type { get; set; } = ""; // IN / OUT / TRANSFER
    public decimal Quantity { get; set; }
    public string Reference { get; set; } = ""; // es: DDT, ordine, produzione
}

public class Supplier
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string VatNumber { get; set; } = "";
    public string Country { get; set; } = "";
}

public class PurchaseOrder
{
    public string Id { get; set; } = "";
    public string SupplierId { get; set; } = "";
    public DateTime Date { get; set; }
    public List<PurchaseOrderLine> Lines { get; set; } = new();
}

public class PurchaseOrderLine
{
    public string ItemId { get; set; } = "";
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}

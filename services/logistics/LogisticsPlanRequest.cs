namespace AIEnterpriseOS.Logistics;

public class LogisticsItem
{
    public string Sku { get; set; } = "";
    public string Description { get; set; } = "";
    public int LengthMm { get; set; }
    public int WidthMm { get; set; }
    public int HeightMm { get; set; }
    public double WeightKg { get; set; }
    public int Quantity { get; set; }
    public bool Stackable { get; set; } = true;
}

public class LogisticsPlanRequest
{
    public string ShipmentType { get; set; } = "domestic";
    public string DestinationCountry { get; set; } = "IT";
    public string[] TransportModes { get; set; } = new[] { "road" };
    public string RoadVehicleType { get; set; } = "";
    public string ContainerType { get; set; } = "";
    public List<LogisticsItem> Items { get; set; } = new();
}

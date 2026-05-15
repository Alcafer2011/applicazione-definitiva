namespace AIEnterpriseOS.Logistics;

public class LoadPlacement
{
    public string ItemSku { get; set; } = "";
    public int Index { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public double RotationDeg { get; set; }
}

public class LogisticsPlanResponse
{
    public string VehicleType { get; set; } = "";
    public double VehicleInnerLengthMm { get; set; }
    public double VehicleInnerWidthMm { get; set; }
    public double VehicleInnerHeightMm { get; set; }
    public List<LoadPlacement> Placements { get; set; } = new();
}

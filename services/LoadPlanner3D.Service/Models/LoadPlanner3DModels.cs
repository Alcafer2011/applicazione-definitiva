namespace AIEnterpriseOS.LoadPlanner3D.Service.Models;

public class Vehicle3D
{
    public string Type { get; set; } = "";
    public int LengthMm { get; set; }
    public int WidthMm { get; set; }
    public int HeightMm { get; set; }
}

public class LoadItem3D
{
    public string Id { get; set; } = "";
    public string Description { get; set; } = "";
    public int LengthMm { get; set; }
    public int WidthMm { get; set; }
    public int HeightMm { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public string Color { get; set; } = "#00AAFF";
    public string Note { get; set; } = "";
}

public class LoadPlan3D
{
    public Vehicle3D Vehicle { get; set; } = new();
    public List<LoadItem3D> Items { get; set; } = new();
}

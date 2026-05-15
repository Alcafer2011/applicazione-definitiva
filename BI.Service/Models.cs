namespace BI.Service.Models;

public class KpiResult
{
    public string Name { get; set; } = "";
    public double Value { get; set; }
}

public class ForecastResult
{
    public string Metric { get; set; } = "";
    public List<double> Predictions { get; set; } = new();
}
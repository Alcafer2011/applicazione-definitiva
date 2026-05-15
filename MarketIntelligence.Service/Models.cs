namespace MarketIntelligence.Service.Models;

public class Competitor
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = "";
    public Dictionary<string, double> Metrics { get; set; } = new();
}

public class StrategySuggestion
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
}
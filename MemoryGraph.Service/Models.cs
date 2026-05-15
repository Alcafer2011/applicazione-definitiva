namespace MemoryGraph.Service.Models;

public class GraphNode
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Type { get; set; } = "";
    public Dictionary<string, object> Data { get; set; } = new();
}

public class GraphRelation
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string From { get; set; } = "";
    public string To { get; set; } = "";
    public string RelationType { get; set; } = "";
}
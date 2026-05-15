namespace SearchEngine.Service.Models;

public class SearchDocument
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Source { get; set; } = "";
    public string Content { get; set; } = "";
    public Dictionary<string, object> Data { get; set; } = new();
}
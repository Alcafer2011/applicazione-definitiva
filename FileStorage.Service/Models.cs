namespace FileStorage.Service.Models;

public class DocumentMetadata
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string FileName { get; set; } = "";
    public string ContentType { get; set; } = "";
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    public string TextContent { get; set; } = "";
    public Dictionary<string, object> Tags { get; set; } = new();
    public int Version { get; set; } = 1;
}
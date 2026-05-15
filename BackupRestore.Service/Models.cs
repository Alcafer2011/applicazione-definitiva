namespace BackupRestore.Service.Models;

public class BackupInfo
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string FileName { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public long Size { get; set; }
}
using MongoDB.Driver;
using BackupRestore.Service.Models;
using System.IO.Compression;
using System.Net.Http.Json;

namespace BackupRestore.Service.Core;

public class BackupCore
{
    private readonly string _backupPath;
    private readonly IMongoCollection<BackupInfo> _meta;
    private readonly HttpClient _http = new();

    public BackupCore()
    {
        _backupPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "bos-backups");
        Directory.CreateDirectory(_backupPath);

        var client = new MongoClient("mongodb://mongo:27017");
        var db = client.GetDatabase("bos-backupmeta");
        _meta = db.GetCollection<BackupInfo>("backups");
    }

    public async Task<BackupInfo> CreateBackup()
    {
        string file = Path.Combine(_backupPath, $"backup_{DateTime.UtcNow:yyyyMMdd_HHmmss}.zip");

        using (var zip = ZipFile.Open(file, ZipArchiveMode.Create))
        {
            zip.CreateEntryFromFile("/data/db/WiredTiger.wt", "mongo_core.wt", CompressionLevel.Fastest);
            zip.CreateEntryFromFile("/data/db/WiredTiger.turtle", "mongo_turtle.wt", CompressionLevel.Fastest);
        }

        var info = new BackupInfo
        {
            FileName = file,
            Size = new FileInfo(file).Length
        };

        await _meta.InsertOneAsync(info);
        await Notify(info);

        return info;
    }

    public async Task<bool> Restore(string file)
    {
        try
        {
            ZipFile.ExtractToDirectory(file, "/data/db", overwriteFiles: true);
            await NotifyRestore(file);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private async Task Notify(BackupInfo info)
    {
        var evt = new
        {
            Type = "backup.created",
            Payload = info
        };

        try
        {
            await _http.PostAsJsonAsync("http://notificationcenter:8080/notifications/publish", evt);
        }
        catch { }
    }

    private async Task NotifyRestore(string file)
    {
        var evt = new
        {
            Type = "backup.restored",
            Payload = new { file }
        };

        try
        {
            await _http.PostAsJsonAsync("http://notificationcenter:8080/notifications/publish", evt);
        }
        catch { }
    }
}
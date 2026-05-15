using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using FileStorage.Service.Models;
using Tesseract;
using System.Net.Http.Json;

namespace FileStorage.Service.Core;

public class FileStorageCore
{
    private readonly IMongoDatabase _db;
    private readonly GridFSBucket _bucket;
    private readonly IMongoCollection<DocumentMetadata> _meta;
    private readonly HttpClient _http = new();

    public FileStorageCore()
    {
        var client = new MongoClient("mongodb://mongo:27017");
        _db = client.GetDatabase("bos-filestorage");
        _bucket = new GridFSBucket(_db);
        _meta = _db.GetCollection<DocumentMetadata>("metadata");
    }

    public async Task<DocumentMetadata> Upload(Stream file, string fileName, string contentType)
    {
        var existing = await _meta.Find(m => m.FileName == fileName).SortByDescending(m => m.Version).FirstOrDefaultAsync();
        int version = existing?.Version + 1 ?? 1;

        var id = await _bucket.UploadFromStreamAsync(fileName, file);

        string text = ExtractText(file);

        var meta = new DocumentMetadata
        {
            Id = id.ToString(),
            FileName = fileName,
            ContentType = contentType,
            TextContent = text,
            Version = version,
            Tags = new Dictionary<string, object>
            {
                { "length", text.Length },
                { "preview", text.Length > 200 ? text[..200] : text }
            }
        };

        await _meta.InsertOneAsync(meta);

        await SendNotification(meta);
        await IndexDocument(meta);

        return meta;
    }

    private string ExtractText(Stream file)
    {
        try
        {
            file.Position = 0;
            using var engine = new TesseractEngine(@"/usr/share/tesseract-ocr/4.00/tessdata", "eng", EngineMode.Default);
            using var img = Pix.LoadFromMemory(ReadFully(file));
            using var page = engine.Process(img);
            return page.GetText();
        }
        catch
        {
            return "";
        }
    }

    private async Task SendNotification(DocumentMetadata meta)
    {
        var evt = new
        {
            Type = "document.uploaded",
            Payload = meta
        };

        try
        {
            await _http.PostAsJsonAsync("http://notificationcenter:8080/notifications/publish", evt);
        }
        catch { }
    }

    private async Task IndexDocument(DocumentMetadata meta)
    {
        var doc = new
        {
            Source = "document",
            Content = meta.TextContent,
            Data = meta
        };

        try
        {
            await _http.PostAsJsonAsync("http://searchengine:8080/search/index/rebuild", doc);
        }
        catch { }
    }

    private byte[] ReadFully(Stream input)
    {
        using MemoryStream ms = new();
        input.CopyTo(ms);
        return ms.ToArray();
    }
}
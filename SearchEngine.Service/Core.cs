using MongoDB.Driver;
using SearchEngine.Service.Models;
using System.Net.Http.Json;

namespace SearchEngine.Service.Core;

public class SearchCore
{
    private readonly IMongoCollection<SearchDocument> _index;
    private readonly HttpClient _http = new();

    public SearchCore()
    {
        var client = new MongoClient("mongodb://mongo:27017");
        var db = client.GetDatabase("bos-search");
        _index = db.GetCollection<SearchDocument>("index");
    }

    public async Task RebuildIndex()
    {
        await _index.DeleteManyAsync(_ => true);

        await IndexFrom("workflow", "http://workflow:8080/workflow/instances");
        await IndexFrom("bi-kpi", "http://bi:8080/bi/kpi");
        await IndexFrom("bi-forecast", "http://bi:8080/bi/forecast");
        await IndexFrom("market", "http://marketintelligence:8080/market/competitors");
        await IndexFrom("autohealing", "http://autohealing:8080/autohealing/status");
        await IndexFrom("notifications", "http://notificationcenter:8080/notifications/history");
        await IndexFrom("audit", "http://audit:8080/audit/history");
        await IndexFrom("memorygraph", "http://memorygraph:8080/memorygraph/related/root");
    }

    private async Task IndexFrom(string source, string url)
    {
        try
        {
            var resp = await _http.GetAsync(url);
            if (!resp.IsSuccessStatusCode) return;

            var json = await resp.Content.ReadAsStringAsync();
            var doc = new SearchDocument
            {
                Source = source,
                Content = json,
                Data = new Dictionary<string, object> { { "raw", json } }
            };

            await _index.InsertOneAsync(doc);
        }
        catch { }
    }

    public async Task<List<SearchDocument>> Query(string q)
    {
        var filter = Builders<SearchDocument>.Filter.Regex("Content", new MongoDB.Bson.BsonRegularExpression(q, "i"));
        return await _index.Find(filter).Limit(50).ToListAsync();
    }
}
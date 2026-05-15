using MongoDB.Driver;
using NotificationCenter.Service.Models;
using System.Net.Http.Json;

namespace NotificationCenter.Service.Core;

public class NotificationCore
{
    private readonly IMongoCollection<NotificationEvent> _events;
    private readonly IMongoCollection<WebhookTarget> _webhooks;
    private readonly HttpClient _http = new();

    public NotificationCore()
    {
        var client = new MongoClient("mongodb://mongo:27017");
        var db = client.GetDatabase("bos-notifications");

        _events = db.GetCollection<NotificationEvent>("events");
        _webhooks = db.GetCollection<WebhookTarget>("webhooks");
    }

    public async Task Publish(NotificationEvent evt)
    {
        await _events.InsertOneAsync(evt);

        var hooks = await _webhooks.Find(w => w.EventTypes.Contains(evt.Type)).ToListAsync();

        foreach (var hook in hooks)
        {
            try
            {
                await _http.PostAsJsonAsync(hook.Url, evt);
            }
            catch
            {
                // ignora errori webhook
            }
        }
    }

    public async Task<List<NotificationEvent>> History()
    {
        return await _events.Find(_ => true).SortByDescending(e => e.Timestamp).Limit(100).ToListAsync();
    }

    public async Task<WebhookTarget> AddWebhook(WebhookTarget hook)
    {
        await _webhooks.InsertOneAsync(hook);
        return hook;
    }
}
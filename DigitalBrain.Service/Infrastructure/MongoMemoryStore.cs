using DigitalBrain.Service.Abstractions;
using DigitalBrain.Service.Models;
using MongoDB.Driver;

namespace DigitalBrain.Service.Infrastructure;

public class MongoMemoryStore : IMemoryStore
{
    private readonly IMongoCollection<MemoryRecord> _collection;

    public MongoMemoryStore(IConfiguration cfg)
    {
        var conn = cfg.GetConnectionString("Mongo") ?? "mongodb://mongo:27017";
        var client = new MongoClient(conn);
        var db = client.GetDatabase("digitalbrain");
        _collection = db.GetCollection<MemoryRecord>("memory");
    }

    public Task AddAsync(MemoryRecord record, CancellationToken ct = default)
        => _collection.InsertOneAsync(record, cancellationToken: ct);

    public async Task<IReadOnlyList<MemoryRecord>> QueryAsync(string tenantId, string? type, CancellationToken ct = default)
    {
        var filter = Builders<MemoryRecord>.Filter.Eq(x => x.TenantId, tenantId);

        if (!string.IsNullOrWhiteSpace(type))
            filter &= Builders<MemoryRecord>.Filter.Eq(x => x.Type, type);

        var list = await _collection.Find(filter).Limit(200).ToListAsync(ct);
        return list;
    }
}
using MongoDB.Driver;
using System.Linq.Expressions;

namespace CRM.Service.Infrastructure;

public class MongoRepository<T> : IMongoRepository<T>
{
    private readonly IMongoCollection<T> _col;

    public MongoRepository(IConfiguration cfg)
    {
        var client = new MongoClient(cfg.GetConnectionString("Mongo"));
        var db = client.GetDatabase("crm");
        _col = db.GetCollection<T>(typeof(T).Name.ToLower());
    }

    public async Task<List<T>> GetAllAsync() =>
        await _col.Find(_ => true).ToListAsync();

    public async Task<T?> GetByIdAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        return await _col.Find(filter).FirstOrDefaultAsync();
    }

    public Task AddAsync(T item) =>
        _col.InsertOneAsync(item);

    public Task UpdateAsync(T item)
    {
        var id = item!.GetType().GetProperty("Id")!.GetValue(item)!.ToString();
        var filter = Builders<T>.Filter.Eq("Id", id);
        return _col.ReplaceOneAsync(filter, item);
    }

    public Task DeleteAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        return _col.DeleteOneAsync(filter);
    }
}
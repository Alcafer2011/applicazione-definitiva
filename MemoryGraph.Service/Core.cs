using MongoDB.Driver;
using MemoryGraph.Service.Models;

namespace MemoryGraph.Service.Core;

public class MemoryGraphCore
{
    private readonly IMongoCollection<GraphNode> _nodes;
    private readonly IMongoCollection<GraphRelation> _relations;

    public MemoryGraphCore()
    {
        var client = new MongoClient("mongodb://mongo:27017");
        var db = client.GetDatabase("bos-memorygraph");

        _nodes = db.GetCollection<GraphNode>("nodes");
        _relations = db.GetCollection<GraphRelation>("relations");
    }

    public async Task<GraphNode> AddNode(GraphNode node)
    {
        await _nodes.InsertOneAsync(node);
        return node;
    }

    public async Task<GraphRelation> AddRelation(GraphRelation rel)
    {
        await _relations.InsertOneAsync(rel);
        return rel;
    }

    public async Task<List<GraphNode>> GetRelated(string nodeId)
    {
        var rels = await _relations.Find(r => r.From == nodeId || r.To == nodeId).ToListAsync();
        var ids = rels.Select(r => r.From == nodeId ? r.To : r.From).ToList();

        return await _nodes.Find(n => ids.Contains(n.Id)).ToListAsync();
    }
}
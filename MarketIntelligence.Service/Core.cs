using MongoDB.Driver;
using MarketIntelligence.Service.Models;

namespace MarketIntelligence.Service.Core;

public class MarketCore
{
    private readonly IMongoCollection<Competitor> _competitors;

    public MarketCore()
    {
        var client = new MongoClient("mongodb://mongo:27017");
        var db = client.GetDatabase("bos-market");
        _competitors = db.GetCollection<Competitor>("competitors");
    }

    public async Task<List<Competitor>> GetCompetitors()
    {
        return await _competitors.Find(_ => true).ToListAsync();
    }

    public async Task<Competitor> AddCompetitor(Competitor c)
    {
        await _competitors.InsertOneAsync(c);
        return c;
    }

    public StrategySuggestion GenerateStrategy(Competitor competitor)
    {
        return new StrategySuggestion
        {
            Title = "Strategia consigliata",
            Description = $"Il competitor {competitor.Name} mostra un trend positivo. Suggerita espansione commerciale e aumento campagne marketing."
        };
    }
}
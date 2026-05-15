using AIEnterpriseOS.AIMemory.Service.Models;

namespace AIEnterpriseOS.AIMemory.Service.Services;

public interface IMemoryEngine
{
    MemoryRecord Save(MemoryRecord record);
    IEnumerable<MemoryRecord> Query(string tenantId, string text);
    IEnumerable<MemoryRecord> GetAll(string tenantId);
}

public class InMemoryMemoryEngine : IMemoryEngine
{
    private readonly List<MemoryRecord> _records = new();

    public MemoryRecord Save(MemoryRecord record)
    {
        record.Score = CalculateScore(record);
        _records.Add(record);
        return record;
    }

    public IEnumerable<MemoryRecord> Query(string tenantId, string text)
    {
        return _records
            .Where(r => r.TenantId == tenantId && r.Content.Contains(text, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(r => r.Score);
    }

    public IEnumerable<MemoryRecord> GetAll(string tenantId)
        => _records.Where(r => r.TenantId == tenantId);

    private double CalculateScore(MemoryRecord record)
    {
        double baseScore = record.Type switch
        {
            MemoryType.Decision => 0.8,
            MemoryType.Preference => 1.0,
            MemoryType.Pattern => 0.9,
            MemoryType.Error => 0.7,
            _ => 0.5
        };

        double recency = 1.0 / (1 + (DateTime.UtcNow - record.CreatedAt).TotalDays);

        return baseScore + recency;
    }
}

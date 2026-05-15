using MongoDB.Driver;
using Audit.Service.Models;
using System.Net.Http.Json;

namespace Audit.Service.Core;

public class AuditCore
{
    private readonly IMongoCollection<AuditEntry> _entries;
    private readonly IMongoCollection<ComplianceRule> _rules;
    private readonly HttpClient _http = new();

    public AuditCore()
    {
        var client = new MongoClient("mongodb://mongo:27017");
        var db = client.GetDatabase("bos-audit");

        _entries = db.GetCollection<AuditEntry>("entries");
        _rules   = db.GetCollection<ComplianceRule>("rules");
    }

    public async Task AddEntry(AuditEntry entry)
    {
        await _entries.InsertOneAsync(entry);
        await CheckCompliance(entry);
    }

    public async Task<List<AuditEntry>> History()
    {
        return await _entries.Find(_ => true).SortByDescending(e => e.Timestamp).Limit(200).ToListAsync();
    }

    public async Task<ComplianceRule> AddRule(ComplianceRule rule)
    {
        await _rules.InsertOneAsync(rule);
        return rule;
    }

    private async Task CheckCompliance(AuditEntry entry)
    {
        var rules = await _rules.Find(_ => true).ToListAsync();

        foreach (var rule in rules)
        {
            if (entry.Action.Contains(rule.Condition, StringComparison.OrdinalIgnoreCase))
            {
                var evt = new
                {
                    Type = "compliance.violation",
                    Payload = new
                    {
                        Rule = rule.Name,
                        Severity = rule.Severity,
                        Entry = entry
                    }
                };

                try
                {
                    await _http.PostAsJsonAsync("http://notificationcenter:8080/notifications/publish", evt);
                }
                catch { }
            }
        }
    }
}
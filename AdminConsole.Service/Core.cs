using MongoDB.Driver;
using AdminConsole.Service.Models;
using System.Net.Http.Json;

namespace AdminConsole.Service.Core;

public class AdminCore
{
    private readonly IMongoCollection<AdminUser> _users;
    private readonly IMongoCollection<AdminRole> _roles;
    private readonly IMongoCollection<LicenseInfo> _licenses;
    private readonly IMongoCollection<AiRule> _aiRules;
    private readonly HttpClient _http = new();

    public AdminCore()
    {
        var client = new MongoClient("mongodb://mongo:27017");
        var db = client.GetDatabase("bos-adminconsole");

        _users = db.GetCollection<AdminUser>("users");
        _roles = db.GetCollection<AdminRole>("roles");
        _licenses = db.GetCollection<LicenseInfo>("licenses");
        _aiRules = db.GetCollection<AiRule>("airules");
    }

    public async Task<AdminUser> CreateUser(AdminUser u)
    {
        await _users.InsertOneAsync(u);
        return u;
    }

    public async Task<AdminRole> CreateRole(AdminRole r)
    {
        await _roles.InsertOneAsync(r);
        return r;
    }

    public async Task<LicenseInfo> AddLicense(LicenseInfo l)
    {
        await _licenses.InsertOneAsync(l);
        return l;
    }

    public async Task<AiRule> AddAiRule(AiRule rule)
    {
        await _aiRules.InsertOneAsync(rule);
        return rule;
    }

    public async Task RestartService(string name)
    {
        try
        {
            await _http.PostAsync($"http://autohealing:8080/autohealing/restart/{name}", null);
        }
        catch { }
    }
}
using MongoDB.Driver;
using ApiGateway.Service.Models;
using System.IdentityModel.Tokens.Jwt;

namespace ApiGateway.Service.Core;

public class GatewayCore
{
    private readonly IMongoCollection<RouteRule> _rules;

    public GatewayCore()
    {
        var client = new MongoClient("mongodb://mongo:27017");
        var db = client.GetDatabase("bos-gateway");
        _rules = db.GetCollection<RouteRule>("rules");
    }

    public async Task<List<RouteRule>> GetRules() =>
        await _rules.Find(_ => true).ToListAsync();

    public bool IsAllowed(RouteRule rule, JwtSecurityToken token)
    {
        var roles = token.Claims.FirstOrDefault(c => c.Type == "roles")?.Value?.Split(',') ?? Array.Empty<string>();
        return roles.Any(r => rule.AllowedRoles.Contains(r));
    }
}
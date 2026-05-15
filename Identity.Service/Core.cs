using MongoDB.Driver;
using Identity.Service.Models;
using Identity.Service.Security;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Service.Core;

public class IdentityCore
{
    private readonly IMongoCollection<UserAccount> _users;
    private readonly TokenGenerator _tokens = new();

    public IdentityCore()
    {
        var client = new MongoClient("mongodb://mongo:27017");
        var db = client.GetDatabase("bos-identity");
        _users = db.GetCollection<UserAccount>("users");
    }

    public async Task<UserAccount?> GetUser(string username)
    {
        return await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
    }

    public async Task<UserAccount> CreateUser(string username, string password, List<string> roles)
    {
        var hash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password)));

        var user = new UserAccount
        {
            Username = username,
            PasswordHash = hash,
            Roles = roles
        };

        await _users.InsertOneAsync(user);
        return user;
    }

    public async Task<TokenResponse?> Login(LoginRequest req)
    {
        var user = await GetUser(req.Username);
        if (user == null || !user.Active) return null;

        var hash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(req.Password)));
        if (hash != user.PasswordHash) return null;

        return _tokens.Generate(user);
    }
}
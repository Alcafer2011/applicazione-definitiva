using Governance.Service.Models;
using Governance.Service.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Governance.Service.Services;

public class AuthService
{
    private readonly InMemoryRepository<User> _users;
    private readonly IConfiguration _cfg;

    public AuthService(InMemoryRepository<User> users, IConfiguration cfg)
    {
        _users = users;
        _cfg = cfg;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest req)
    {
        var all = await _users.GetAllAsync();
        var user = all.FirstOrDefault(u => u.Username == req.Username);

        if (user == null) return null;
        if (user.PasswordHash != BCrypt.Net.BCrypt.HashPassword(req.Password, user.PasswordHash)) return null;

        var key = Encoding.UTF8.GetBytes(_cfg["Jwt:Key"]!);
        var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: new[]
            {
                new Claim("uid", user.Id),
                new Claim("role", user.RoleId)
            },
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds
        );

        return new LoginResponse(
            new JwtSecurityTokenHandler().WriteToken(token),
            user.Id,
            user.RoleId
        );
    }
}